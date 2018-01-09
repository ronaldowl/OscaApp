using Microsoft.Extensions.Configuration;
using OscaApp.framework.Models;
using OscaApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;


namespace OscaApp.Data
{
    public   class SqlGenericData
    {
        public  string conectService { get; set; }
        


        public SqlGenericData()
        {
            this.conectService = @"Server=tcp:oscadbservices.database.windows.net,1433;Initial Catalog=OscadbData;Persist Security Info=False;User ID=ronaldowl_admin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
        public Relacao RetornaCliente(Guid id)
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
                        CommandText = "select O.id , o.nomeCliente ,  status from Cliente as O where O.id = '" + id.ToString() + "'",
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
        


    }
}
