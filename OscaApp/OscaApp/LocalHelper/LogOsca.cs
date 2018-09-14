using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using OscaApp.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OscaApp.framework
{
    public class LogOsca 
    {

         
      
        public string conectService { get; set; }
        public IConfiguration Configuration { get; }

        public LogOsca()
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            this.Configuration = Configuration;
            this.conectService = Configuration.GetConnectionString("databaseService");
        }


        public Guid id { get; set; }

        public int codigoEntidade { get; set; }

        public int codigoErro { get; set; }

        public Guid idUsuario { get; set; }

        public Guid idOrganizacao { get; set; }

        public DateTime dataCriacao { get; set; }

        public string evento { get; set; }

        public string mensagem { get; set; }


        public void GravaLog(int codigoErro,int cogidoEntidade, Guid idUsuairo, Guid idOrganizacao, string evento, string mensagem)
        {


            string comando = "insert into LogOsca (codigoErro, codigoEntidade, idUsuario, idOrganizacao,evento, mensagem) values('" + codigoErro.ToString() + "', '" + codigoEntidade.ToString() + "', '" + idUsuario.ToString() + "', '" + idOrganizacao.ToString() + "', '" + evento + "', '" + mensagem + "')";

          
                using (SqlConnection Connection = new SqlConnection(conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = comando,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    _Command.ExecuteScalar();
                    Connection.Close();
                };
         
        }


        public LogOsca get(Guid id)
        {

            LogOsca retorno = new LogOsca();
            string comando = "select id, codigoEntidade, evento, mensagem, idUsuario, dataCriacao, codigoErro from LogOsca where id =   '" + id.ToString() + "'";

            SqlDataReader dataReader;
         

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = comando,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            LogOsca item = new LogOsca();

                            //Execução do Parse para LogOsca

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigoEntidade = Convert.ToInt32(dataReader["codigoEntidade"]);
                            item.evento = dataReader["evento"].ToString();
                            item.mensagem = dataReader["mensagem"].ToString();
                            item.idUsuario = new Guid(dataReader["idUsuario"].ToString());
                            item.dataCriacao = Convert.ToDateTime(dataReader["dataCriacao"].ToString());
                            item.codigoErro = Convert.ToInt32(dataReader["codigoErro"]);

                            retorno = item;
                        }
                    }
                    Connection.Close();
                };
          

            return retorno;
        }

        public List<LogOsca> getAll(Guid idOrganizacao)
        {

            List<LogOsca> retorno = new List<LogOsca>();
            string comando = "select id, codigoEntidade, evento, mensagem, idUsuario, dataCriacao, codigoErro from LogOsca where idOrganizacao =   '" + idOrganizacao.ToString() + "'";

            SqlDataReader dataReader;
          

                using (SqlConnection Connection = new SqlConnection(this.conectService))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = comando,
                        CommandType = CommandType.Text
                    };

                    Connection.Open();
                    dataReader = _Command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {

                            LogOsca item = new LogOsca();

                            //Execução do Parse para LogOsca

                            item.id = new Guid(dataReader["id"].ToString());
                            item.codigoEntidade = Convert.ToInt32(dataReader["codigoEntidade"]);
                            item.evento = dataReader["evento"].ToString();
                            item.mensagem = dataReader["mensagem"].ToString();
                            item.idUsuario = new Guid(dataReader["idUsuario"].ToString());
                            item.dataCriacao = Convert.ToDateTime(dataReader["dataCriacao"].ToString());
                            item.codigoErro = Convert.ToInt32(dataReader["codigoErro"]);

                            retorno.Add(item);
                        }
                    }
                    Connection.Close();
                };
          

            return retorno;
        }
    }
}