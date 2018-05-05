using Microsoft.Extensions.Configuration;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OscaFramework.MicroServices
{
   public   class SqlGenericData
    {
        public  string conectService { get; set; }
        public IConfiguration Configuration { get; }

        public SqlGenericData()
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            this.Configuration = Configuration; 
            this.conectService = Configuration.GetConnectionString("databaseService");
        }

        public ClientePotencial RetornaClientePotencial(Guid id)
        {
            ClientePotencial retorno = new ClientePotencial();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeCliente ,  statusLead, email, telefone, celular,anotacao, sexo from ClientePotencial as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.nomeCliente = dataReader["nomeCliente"].ToString();
                            retorno.statusLead = (CustomEnumStatus.StatusLead)Convert.ToInt32(dataReader["statusLead"].ToString());
                            retorno.email = dataReader["email"].ToString();
                            retorno.telefone =  dataReader["telefone"].ToString();
                            retorno.celular = dataReader["celular"].ToString();
                            retorno.anotacao = dataReader["anotacao"].ToString();
                            retorno.sexo = (CustomEnum.Sexo)Convert.ToInt32(dataReader["sexo"].ToString());                                    
                             
                            
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }

        public List<Profissional> RetornaProfissional(Guid idOrganizacao)
        {
            List<Profissional> retorno = new List<Profissional>();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeProfissional from Profissional as O where O.idOrganizacao = '" + idOrganizacao.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Profissional item = new Profissional();

                            item.id = new Guid(dataReader["id"].ToString());
                            item.nomeProfissional = dataReader["nomeProfissional"].ToString();
                            retorno.Add(item);
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Servico RetornaServico(Guid id)
        {
            Servico retorno = new Servico();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeServico ,  status, codigo, valor from Servico as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.nomeServico = dataReader["nomeServico"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.valor = Convert.ToDecimal(dataReader["valor"]);

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public ListaPreco RetornaListaPreco(Guid id)
        {
            ListaPreco retorno = new ListaPreco();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nome ,  status from ListaPreco as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.nome = dataReader["nome"].ToString();                        
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());
                          
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Produto RetornaProduto(Guid id)
        {
            Produto retorno = new Produto();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nome ,  status, codigo, fabricante, valorCompra, quantidade, codigoFabricante from Produto as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.nome = dataReader["nome"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.fabricante = dataReader["fabricante"].ToString();
                            retorno.valorCompra = Convert.ToDecimal(dataReader["valorCompra"]);
                            retorno.quantidade = Convert.ToInt32(dataReader["quantidade"]);
                            retorno.codigoFabricante = dataReader["codigoFabricante"].ToString();
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Cliente RetornaCliente(Guid id)
        {
            Cliente retorno = new Cliente();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeCliente, o.cnpj_cpf ,o.codigo, o.telefone,  status from Cliente as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.nomeCliente = dataReader["nomeCliente"].ToString();
                            retorno.cnpj_cpf = dataReader["cnpj_cpf"].ToString();
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.telefone = dataReader["telefone"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public List<Relacao> RetornaTodasRelacaoProfissional(Guid idOrganizacao)
        {
            List<Relacao> retorno = new List<Relacao>();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select id,nomeProfissional from Profissional where idOrganizacao = '" + idOrganizacao.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Relacao item = new Relacao();
                            item.id = new Guid(dataReader["id"].ToString());
                            item.idName = dataReader["nomeProfissional"].ToString();
                            retorno.Add(item);
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoProfissional(Guid id)
        {
             Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select id,nomeProfissional from Profissional where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nomeProfissional"].ToString();                             
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoPedido(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from Pedido where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.codigo = dataReader["codigoPedido"].ToString();

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoAgendamentoByIdReferencia(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from Agendamento where idReferencia = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.codigo = dataReader["codigo"].ToString();

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoBanco(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from banco where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nome"].ToString();

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoAtendimento(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from atendimento where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());                          
                            retorno.codigo = dataReader["codigo"].ToString();


                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoContato(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from contato where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nome"].ToString();

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }      
        public Relacao RetornaRelacaoProduto(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nome ,  status, codigo, fabricante, valorCompra, quantidade, codigoFabricante from Produto as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nome"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());                      
                        }
                    }
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoListaPreco(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nome,   status from ListaPreco as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());                           
                            retorno.idName = dataReader["nome"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoCliente(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeCliente, o.codigo,  status from Cliente as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nomeCliente"].ToString();
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoCategoriaManutencao(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from CategoriaManutencao  where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nome"].ToString();
           
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoPedidoPorProdutoPedido(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.idPedido ,  status from ProdutoPedido as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["idPedido"].ToString());
                         
                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoUsuario(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {


                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select id, UserName from AspNetUsers where id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["UserName"].ToString();

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoServico(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeServico, o.codigo,  status from Servico as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nomeServico"].ToString();
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoOrdemServico(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.codigo,  status from OrdemServico as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["codigo"].ToString();
                            retorno.codigo = dataReader["codigo"].ToString();
                            retorno.status = (CustomEnumStatus.Status)Convert.ToInt32(dataReader["status"].ToString());

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoOrdemServicoPorIDServicoOrdem(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.idOrdemServico ,  status from ServicoOrdem as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["idOrdemServico"].ToString());

                        }
                    }
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public Relacao RetornaRelacaoOrdemServicoPorIDProdutoOrdem(Guid id)
        {
            Relacao retorno = new Relacao();
            SqlDataReader dataReader;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.idOrdemServico ,  status from produtoOrdem as O where O.id = '" + id.ToString() + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["idOrdemServico"].ToString());

                        }
                    }
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
