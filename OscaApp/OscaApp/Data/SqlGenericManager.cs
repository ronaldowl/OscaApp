
using OscaApp.framework.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using OscaApp.Models;


namespace OscaApp.Data
{
    public   class SqlGenericManager : ISqlGenericManager
     {

        public string conectStringManager { get; set; }

        public SqlGenericManager()
        {
            this.conectStringManager = @"Server=tcp:oscadbservices.database.windows.net,1433;Initial Catalog=OscadbManager;Persist Security Info=False;User ID=ronaldowl_admin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; 
        }

        public  Guid CriaOrganizacao(string org, string email)
        {
            object retorno;
            try
            {
                using (SqlConnection Connection = new SqlConnection(conectStringManager))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "osc_CriaNovaOrganizacao",
                        CommandType = CommandType.StoredProcedure
                    };

                    _Command.Parameters.AddWithValue("org", org);
                    _Command.Parameters.AddWithValue("Email", email);
                 
                    Connection.Open();
                    retorno = _Command.ExecuteScalar();
                    Connection.Close();
                };
            }
            catch (SqlException ex)
            {
                throw;
            }

            return new Guid (retorno.ToString());
        }

        public bool ExisteOrganizacao(string org,out Guid id)
        {
            try
            {
                object retorno;

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id from Organizacao as O where O.nomeLogin = '" + org + "'",
                        CommandType = CommandType.Text
                    };
                 
                    Connection.Open();
                    retorno =  _Command.ExecuteScalar();
                    Connection.Close();

                    if (retorno != null)
                    {
                        id = new Guid(retorno.ToString());
                        return true;
                    }
                };
            }
            catch (SqlException ex)
            {
                throw;
            }
            return false;
        }

        public Organizacao RetornaOrganizacao(Guid idOrg)
        { 
              Organizacao retorno = new Organizacao();
              SqlDataReader dataReader;
            try
            {
             

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
                {

                    var _Command = new SqlCommand()
                    {
                        Connection = Connection,
                        CommandText = "select O.id , o.nomeLogin ,o.nomeAmigavel, status, statusOrg from Organizacao as O where O.id = '" + idOrg.ToString() + "'",
                        CommandType = CommandType.Text
                    };
                 
                    Connection.Open();
                    dataReader =  _Command.ExecuteReader();


                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            retorno.id = new Guid(dataReader["id"].ToString()); 
                            retorno.nomeLogin = dataReader["nomeLogin"].ToString();
                            retorno.nomeAmigavel = dataReader["nomeAmigavel"].ToString();
                            retorno.status = (CustomEnum.Status)Convert.ToInt32(dataReader["status"].ToString());
                            retorno.statusOrg = (CustomEnum.StatusOrg)Convert.ToInt32(dataReader["statusOrg"].ToString());

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

        public Relacao RetornaContextPage(string email, string org)
        {

            Relacao retorno = new Relacao();
            try
            {
                SqlDataReader dataReader;              

                using (SqlConnection Connection = new SqlConnection(conectStringManager))
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
                            retorno.idOrganizacao = new Guid(dataReader["idOrganizacao"].ToString());
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

        //public static String RetornaSelectResult()
        //{

        //    using (var conn = new SqlConnection(connectionString))
        //    using (var cmd = new SqlCommand("select * from Products", conn))
        //    {
        //        var dt = new DataTable();
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            da.Fill(dt);
        //        }
        //    }

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        int productId = Convert.ToInt32(row[0]);
        //        string productName = row["ProductName"].ToString();
        //    }



        //}

    }
}
