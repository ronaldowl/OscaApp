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

        public Boolean ConsultaCnpj_CpfDuplicado(string valor, string idOrganizacao)
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


    }
}
