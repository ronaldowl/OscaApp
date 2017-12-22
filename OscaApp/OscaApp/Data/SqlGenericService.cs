using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;


namespace OscaApp.Data
{
    public   class SqlGenericService
    {
        public  string conectService { get; set; }
        public   string conectManager { get; set; }


        public SqlGenericService()
        {
            this.conectService = @"Server=tcp:oscadbservices.database.windows.net,1433;Initial Catalog=OscadbData;Persist Security Info=False;User ID=ronaldowl_admin;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        }

        public  void InicializaOrg(string idOrg)
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
