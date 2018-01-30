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
            this.conectService = @"Data Source=SQL5037.site4now.net;Initial Catalog=DB_A2B7EF_OSCADBDATA;User Id=DB_A2B7EF_OSCADBDATA_admin;Password=P@ssw0rd;";

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
        

    }
}
