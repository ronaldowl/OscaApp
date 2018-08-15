using Microsoft.Extensions.Configuration;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OscaFramework.MicroServices
{
    public   class SqlGenericRules
    {
        public  string conectService { get; set; }
        public IConfiguration Configuration { get; }

        public SqlGenericRules()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            this.Configuration = Configuration;            
            this.conectService = Configuration.GetConnectionString("databaseService");
        }

        public List<ProdutoBalcao> ConsultaProduto(string filtro, string idLista)
        {           
            List<ProdutoBalcao> Listaretorno = new List<ProdutoBalcao>();
            SqlDataReader dataReader;

            string SelectProduto = "select Lp.id idItemLista, P.id, P.codigo, P.nome, Lp.valor, P.quantidade,isnull( P.fabricante,'Ausente')fabricante, isnull(P.modelo, 'Ausente')modelo from itemListaPreco LP inner join produto as P on P.id = Lp.idProduto where Lp.idListaPreco = '" + idLista +"' and(P.nome like('%"+ filtro +"%')  or P.codigo = '"+ filtro + "' or P.codigoBarra = '"+ filtro +"')";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {                 

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };            

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            ProdutoBalcao retorno = new ProdutoBalcao();

                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idItemListaPreco = new Guid(dataReader["idItemLista"].ToString());
                            retorno.nome = dataReader["nome"].ToString();
                            retorno.fabricante = dataReader["fabricante"].ToString();
                            retorno.modelo = dataReader["modelo"].ToString();
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());
                            retorno.valor = Convert.ToDecimal(dataReader["valor"].ToString());
                            Listaretorno.Add(retorno);
                        }
                    }
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return Listaretorno;

        }
        public Boolean ConsultaCnpj_CpfDuplicado(string valor, string idOrganizacao, string id)
        {
            bool sucesso = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    object retorno = null;

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_Cnpj_CfpExistente",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("valor", valor);
                    _Command.Parameters.AddWithValue("idOrganizacao", idOrganizacao);
                    _Command.Parameters.AddWithValue("idCliente", id);

                    Connection.Open();
                    retorno = _Command.ExecuteScalar();

                    if (retorno != null  ) sucesso = true;

                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return sucesso;

        }
        public Boolean ConsultaEmailDuplicado(string valor, string idOrganizacao)
        {
            bool sucesso = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    object retorno = null;

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_EmailExistente",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("valor", valor);
                    _Command.Parameters.AddWithValue("idOrganizacao", idOrganizacao);

                    Connection.Open();
                    retorno = _Command.ExecuteScalar();

                    if (retorno != null) sucesso = true;

                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return sucesso;

        }

        public bool CancelaFaturamentoBalcao(string idReferencia)
        {
            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = @"update Faturamento set status = 0 where idReferencia = '" + idReferencia + "'"
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();
                    //Fechando conexao após tratar o retorno
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public string RetornaValorDiarioVenda(Guid idOrg)
        {
            string retorno = String.Empty; 
         
            string SelectProduto = "Select isnull(sum(valorTotal),0) from BalcaoVendas where Cast(criadoEm as date) = Cast(getdate() as date) and StatusBalcaoVendas = 1  and idOrganizacao =  '" + idOrg + "'";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                   var  valor =  _Command.ExecuteScalar();
                    retorno = valor.ToString(); 
                
                    Connection.Close();

                    
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;

        }

        public string RetornaValorEmAbertoCliente(string idCliente)
        {
            string retorno = String.Empty;

            string SelectProduto = "Select isnull(sum(valor),0) from ContasReceber where idCliente = '" + idCliente + "' and statusContaReceber in(0, 3)   ";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    var valor = _Command.ExecuteScalar();
                    retorno = valor.ToString();

                    Connection.Close();


                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;

        }
        public string RetornaValorEmAberto(string idOrg)
        {
            string retorno = String.Empty;

            string SelectProduto = "Select isnull(sum(valor),0) from ContasReceber where idOrganizacao = '" + idOrg + "' and statusContaReceber in(0, 3)   ";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    var valor = _Command.ExecuteScalar();
                    retorno = valor.ToString();

                    Connection.Close();


                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;

        }

        public string RetornaValorRecebidoCliente(string idCliente)
        {
            string retorno = String.Empty;

            string SelectProduto = "Select isnull(sum(valor),0) from ContasReceber where idCliente = '" + idCliente + "' and statusContaReceber = 1   ";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    var valor = _Command.ExecuteScalar();
                    retorno = valor.ToString();

                    Connection.Close();


                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;

        }
        public string RetornaValorDiarioVendaDinheiro(Guid idOrg)
        {
            string retorno = String.Empty;

            string SelectProduto = "Select isnull(sum(valor),0) from BalcaoVendas where tipoPagamento = 1 and Cast(criadoEm as date) = Cast(getdate() as date) and StatusBalcaoVendas = 1  and idOrganizacao =  '" + idOrg + "'";

            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = SelectProduto,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    var valor = _Command.ExecuteScalar();
                    retorno = valor.ToString();

                    Connection.Close();


                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;

        }

    }
}
