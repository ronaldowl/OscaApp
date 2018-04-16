using Microsoft.Extensions.Configuration;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace OscaFramework.MicroServices
{
    public   class SqlGeneric
    {
        public  string conectService { get; set; }
        public IConfiguration Configuration { get; }

        public SqlGeneric()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            this.Configuration = Configuration;            
            this.conectService = Configuration.GetConnectionString("databaseService");
        }
        public Boolean ConsultaUsuarioEmpresa(string nomeEmpresa, string nomeLogin)
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
                        CommandText = "osc_ConsultaUsuarioEmpresa",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("nomeEmpresa", nomeEmpresa);
                    _Command.Parameters.AddWithValue("loginName", nomeLogin);
                   
                    Connection.Open();
                    retorno =  _Command.ExecuteScalar();

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

        public void InicializaOrg(string idOrg, string nomeLogin, string nomeUsuario)
        {           
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_InializaNovaOrg",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("idOrg", idOrg);
                    _Command.Parameters.AddWithValue("loginName", nomeLogin);
                    _Command.Parameters.AddWithValue("nomeUsuario", nomeUsuario);                   
                    
                    Connection.Open();
                    _Command.ExecuteScalar();
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            
        }
        public String RetornaNovaPosicao(int Entidade , Guid idOrganizacao)
        {
            object retorno;         
            
            try
            {

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaUltimaPosicao",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("idOrganizacao", idOrganizacao);
                    _Command.Parameters.AddWithValue("entidade", Entidade);
                    
                    Connection.Open();
                    retorno = _Command.ExecuteScalar();
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno.ToString();
        }
        public Relacao RetornaContextPage(string email, string org)
        {

            Relacao retorno = new Relacao();
            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaContextoOrg",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("Org", org);
                    _Command.Parameters.AddWithValue("Email", email);

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["idUsuario"].ToString());
                            retorno.idOrganizacao = new Guid(dataReader["id"].ToString());
                            retorno.idName = dataReader["nomeUsuario"].ToString();
                            retorno.organizacao = dataReader["nomeOrganizacao"].ToString();
                        }
                    }

                    //Fechando conexao após tratar o retorno
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }

        public List<Atendimento> RetornaAtendimentosMes( string mes, string ano, string idProfissional, string idOrg)
        {
            SqlDataReader dataReader;
            List<Atendimento> retorno = new List<Atendimento>();
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaAtendimentosMes",
                        CommandType = CommandType.StoredProcedure
                    };
                    _Command.Parameters.AddWithValue("mes", mes);
                    _Command.Parameters.AddWithValue("ano", ano);
                    _Command.Parameters.AddWithValue("idProfissional", idProfissional);   
                    _Command.Parameters.AddWithValue("idOrg", idOrg);

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Atendimento item = new Atendimento();

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigo = dataReader["codigo"].ToString();
                            item.dataAgendada = Convert.ToDateTime(dataReader["dataAgendada"].ToString());
                            item.idCliente = new Guid(dataReader["idCliente"].ToString());
                            item.idServico = new Guid(dataReader["idServico"].ToString());
                   
                            item.statusAtendimento = (CustomEnumStatus.StatusAtendimento)Convert.ToInt32(dataReader["statusAtendimento"].ToString());
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
        public List<Agendamento> RetornaAgendamentosMes(string mes, string ano, string idProfissional, string idOrg)
        {
            SqlDataReader dataReader;
            List<Agendamento> retorno = new List<Agendamento>();
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaAtendimentosMes",
                        CommandType = CommandType.StoredProcedure
                    };
                    _Command.Parameters.AddWithValue("mes", mes);
                    _Command.Parameters.AddWithValue("ano", ano);
                    _Command.Parameters.AddWithValue("idProfissional", idProfissional);
                    _Command.Parameters.AddWithValue("idOrg", idOrg);

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Agendamento item = new Agendamento();

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigo = dataReader["codigo"].ToString();
                            item.dataAgendada = Convert.ToDateTime(dataReader["dataAgendada"].ToString());
                            item.idCliente = new Guid(dataReader["idCliente"].ToString());
                            item.id = new Guid(dataReader["idServico"].ToString());

                            item.statusAgendamento = (CustomEnumStatus.StatusAgendamento)Convert.ToInt32(dataReader["statusAgendamento"].ToString());
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

        public List<Atendimento> RetornaAtendimentosDia(string  data, string idProfissional, string idOrg)
        {
            SqlDataReader dataReader;
            List<Atendimento> retorno = new List<Atendimento>();
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaAtendimentosDia",
                        CommandType = CommandType.StoredProcedure
                    };
                    _Command.Parameters.AddWithValue("dia", data);                   
                    _Command.Parameters.AddWithValue("idProfissional", idProfissional);
                    _Command.Parameters.AddWithValue("idOrg", idOrg);

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Atendimento item = new Atendimento();

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigo = dataReader["codigo"].ToString();
                            item.dataAgendada = Convert.ToDateTime(dataReader["dataAgendada"].ToString());
                            item.idCliente = new Guid(dataReader["idCliente"].ToString());
                            item.idServico = new Guid(dataReader["idServico"].ToString());                       
                            item.statusAtendimento = (CustomEnumStatus.StatusAtendimento)Convert.ToInt32(dataReader["statusAtendimento"].ToString());
                            item.titulo = dataReader["titulo"].ToString();
                      
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
        public List<Agendamento> RetornaAgendamentosDia(string data, string idProfissional, string idOrg)
        {
            SqlDataReader dataReader;
            List<Agendamento> retorno = new List<Agendamento>();
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaAtendimentosDia",
                        CommandType = CommandType.StoredProcedure
                    };
                    _Command.Parameters.AddWithValue("dia", data);
                    _Command.Parameters.AddWithValue("idProfissional", idProfissional);
                    _Command.Parameters.AddWithValue("idOrg", idOrg);

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Agendamento item = new Agendamento();

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigo = dataReader["codigo"].ToString();
                            item.dataAgendada = Convert.ToDateTime(dataReader["dataAgendada"].ToString());
                            item.idCliente = new Guid(dataReader["idCliente"].ToString());
                            item.idReferencia = new Guid(dataReader["idReferencia"].ToString());
                            item.statusAgendamento = (CustomEnumStatus.StatusAgendamento)Convert.ToInt32(dataReader["statusAgendamento"].ToString());
                        

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


        //*********** Retorna dados das tabelas auxiliares **********************
        public List<CategoriaManutencao> RetornaCategoriaManutencao()
        {
            List<CategoriaManutencao> retorno = new List<CategoriaManutencao>();
            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from CategoriaManutencao",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            CategoriaManutencao item = new CategoriaManutencao();
                            item.id = new Guid(dataReader["id"].ToString());
                            item.nome = dataReader["nome"].ToString();
                            retorno.Add(item);
                        }
                    }

                    //Fechando conexao após tratar o retorno
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }
        public List<Relacao> RetornaRelacaoCategoriaManutencao()
        {
            List<Relacao> retorno = new List<Relacao>();
            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select * from CategoriaManutencao",
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
                            item.idName = dataReader["nome"].ToString();
                            retorno.Add(item);
                        }
                    }

                    //Fechando conexao após tratar o retorno
                    Connection.Close();

                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return retorno;
        }

        public string RetornaidProfissionalPorIdUsuario(string IdUser)
        {
             string idProfissional = String.Empty;
            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select id from Profissional where idUsuario = '" + IdUser + "'",
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            idProfissional = new Guid(dataReader["id"].ToString()).ToString();
                            break;
                        }
                    }

                    //Fechando conexao após tratar o retorno
                    Connection.Close();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return idProfissional;
        }
        //********** Atualiza valores nas tabelas diretamente *********
        public bool SetStates(Relacao Registro)
        {


            try
            {
                SqlDataReader dataReader;

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {
                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_RetornaContextoOrg"

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

        //public void InicializaOrg(string idOrg, string nomeLogin, string nomeUsuario)
        //{
        //    try
        //    {
        //        using (SqlConnection Connection = new SqlConnection(conectService))
        //        {

        //            var _Command = new SqlCommand()
        //            {
        //                Connection = Connection,
        //                CommandText = "osc_InializaNovaOrg",
        //                CommandType = CommandType.StoredProcedure
        //            };

        //            _Command.Parameters.AddWithValue("idOrg", idOrg);
        //            _Command.Parameters.AddWithValue("loginName", nomeLogin);
        //            _Command.Parameters.AddWithValue("nomeUsuario", nomeUsuario);

        //            Connection.Open();
        //            _Command.ExecuteScalar();
        //            Connection.Close();
        //        };
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }

        //}

        //public String RetornaNovaPosicao(int Entidade, Guid idOrganizacao)
        //{
        //    object retorno;

        //    try
        //    {

        //        using (SqlConnection Connection = new SqlConnection(this.conectService))
        //        {

        //            var _Command = new SqlCommand()
        //            {
        //                Connection = Connection,
        //                CommandText = "osc_RetornaUltimaPosicao",
        //                CommandType = CommandType.StoredProcedure
        //            };

        //            _Command.Parameters.AddWithValue("idOrganizacao", idOrganizacao);
        //            _Command.Parameters.AddWithValue("entidade", Entidade);

        //            Connection.Open();
        //            retorno = _Command.ExecuteScalar();
        //            Connection.Close();
        //        };
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //    return retorno.ToString();
        //}

        //public Relacao RetornaContextPage(string email, string org)
        //{

        //    Relacao retorno = new Relacao();
        //    try
        //    {
        //        SqlDataReader dataReader;

        //        using (SqlConnection Connection = new SqlConnection(this.conectService))
        //        {
        //            var _Command = new SqlCommand()
        //            {
        //                Connection = Connection,
        //                CommandText = "osc_RetornaContextoOrg",
        //                CommandType = CommandType.StoredProcedure
        //            };

        //            _Command.Parameters.AddWithValue("Org", org);
        //            _Command.Parameters.AddWithValue("Email", email);

        //            Connection.Open();
        //            dataReader = _Command.ExecuteReader();

        //            if (dataReader.HasRows)
        //            {
        //                while (dataReader.Read())
        //                {
        //                    retorno.id = new Guid(dataReader["idUsuario"].ToString());
        //                    retorno.idOrganizacao = new Guid(dataReader["idOrganizacao"].ToString());
        //                    retorno.idName = dataReader["nomeUsuario"].ToString();
        //                    retorno.organizacao = dataReader["nomeOrganizacao"].ToString();
        //                }
        //            }

        //            //Fechando conexao após tratar o retorno
        //            Connection.Close();

        //        };
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //    return retorno;
        //}


        //public String RetornaNovaPosicao(int Entidade, Guid idOrganizacao)
        //{
        //    object retorno;

        //    try
        //    {

        //        using (SqlConnection Connection = new SqlConnection(this.conectService))
        //        {

        //            var _Command = new SqlCommand()
        //            {
        //                Connection = Connection,
        //                CommandText = "osc_RetornaUltimaPosicao",
        //                CommandType = CommandType.StoredProcedure
        //            };

        //            _Command.Parameters.AddWithValue("idOrganizacao", idOrganizacao);
        //            _Command.Parameters.AddWithValue("entidade", Entidade);

        //            Connection.Open();
        //            retorno = _Command.ExecuteScalar();
        //            Connection.Close();
        //        };
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //    return retorno.ToString();
        //}
        //public void InicializaOrg(string idOrg)
        //{
        //    try
        //    {
        //        using (SqlConnection Connection = new SqlConnection(conectService))
        //        {

        //            var _Command = new SqlCommand()
        //            {
        //                Connection = Connection,
        //                CommandText = "osc_InializaNovaOrg",
        //                CommandType = CommandType.StoredProcedure
        //            };

        //            _Command.Parameters.AddWithValue("idOrg", idOrg);

        //            Connection.Open();
        //            _Command.ExecuteScalar();
        //            Connection.Close();
        //        };
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }

        //}



    }
}
