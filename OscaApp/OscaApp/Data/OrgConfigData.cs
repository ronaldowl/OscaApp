﻿using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class OrgConfigData : IOrgConfigData
    {
        private ContexDataService db;

          public OrgConfigData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(OrgConfig modelo)
        {
            try
            {
                db.OrgsConfig.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(OrgConfig modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                     = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public OrgConfig Get(Guid id, Guid idOrg)
        {
            List<OrgConfig> retorno = new List<OrgConfig>();
            try
            {
                retorno = db.OrgsConfig.FromSql("SELECT * FROM OrgConfig WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }      
    }
}