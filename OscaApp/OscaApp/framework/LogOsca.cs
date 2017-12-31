using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using OscaApp.Models;

namespace OscaApp.framework
{
    public class LogOsca 
    {

        public string conectStringManager { get; set; }

        public LogOsca()
        {
            this.conectStringManager = @"Server=tcp:oscadbservices.database.windows.net,1433;Initial Catalog=OscadbManager;Persist Security Info=False;User ID=ronaldowl_admin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        public Guid id { get; set; }

        public int codigoEntidade { get; set; }

        public Guid idUsuario { get; set; }

        public Guid idOrganizacao { get; set; }

        public DateTime dataCriacao { get; set; }

        public string evento { get; set; }

        public string mensagem { get; set; }


        public void GravaLog(int cogidoEntidade, Guid idUsuairo, Guid idOrganizacao, string evento, string mensagem)
        {


            string comando = "insert into LogOsca (codigoEntidade, idUsuario, idOrganizacao,evento, mensagem) values('" + codigoEntidade.ToString() + "', '" + idUsuario.ToString() + "', '" + idOrganizacao.ToString() + "', '" + evento + "', '" + mensagem + "')";

            try
            {

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
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
            catch (SqlException ex)
            {

            }
        }


        public LogOscaModel get(Guid id)
        {

            LogOscaModel retorno = new LogOscaModel();
            string comando = "select id, codigoEntidade, evento, mensagem, idUsuario, dataCriacao from LogOsca where id =   '" + id.ToString() + "'";

            SqlDataReader dataReader;
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
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

                            LogOscaModel item = new LogOscaModel();

                            //Execução do Parse para LogOsca

                            item.id = new Guid(dataReader["id"].ToString());
                            item.CodigoEntidade = Convert.ToInt32(dataReader["codigoEntidade"]);
                            item.Evento = dataReader["evento"].ToString();
                            item.Mensagem = dataReader["mensagem"].ToString();
                            item.IdUsuario = new Guid(dataReader["idUsuario"].ToString());
                            item.DataCriacao = Convert.ToDateTime(dataReader["dataCriacao"].ToString());

                            retorno = item;
                        }
                    }
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {

            }

            return retorno;
        }

        public List<LogOsca> getAll(Guid idOrganizacao)
        {

            List<LogOsca> retorno = new List<LogOsca>();
            string comando = "select id, codigoEntidade, evento, mensagem, idUsuario, dataCriacao from LogOsca where idOrganizacao =   '" + idOrganizacao.ToString() + "'";

            SqlDataReader dataReader;
            try
            {

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
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

                            retorno.Add(item);
                        }
                    }
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {

            }

            return retorno;
        }
    }
}