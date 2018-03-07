﻿using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.RulesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.Data;
using OscaFramework.MicroServices;
using Microsoft.AspNetCore.Http;
using OscaApp.Data;

namespace OscaApp.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly SqlGeneric sqlServices;
        private ContextPage contexto;

        public CalendarioController(SqlGeneric _sqlServices, IHttpContextAccessor httpContext)
        {
            sqlServices = _sqlServices;
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [HttpGet]
        public ViewResult Mes(int Mes, int Ano )
        {
            Calendario calen = new Calendario();
            string idProfissional = sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString());


            if (Mes > 0)
            {
               calen = CalendarioRules.PreencheMes(Mes, Ano, sqlServices, this.contexto, idProfissional);
            }
            else{

                calen = CalendarioRules.PreencheMes(DateTime.Now.Month, DateTime.Now.Year, sqlServices, this.contexto, idProfissional);
            }          

            return View(calen);
        }

        [HttpPost]
        public ViewResult Mes(Calendario entrada)
        {
            Calendario calen = new Calendario();
         
            calen = CalendarioRules.PreencheMes(entrada.mes, entrada.ano, sqlServices, contexto,entrada.idProfissional );          
           
            return View(calen);
        }

        [HttpGet]
        public ViewResult Dia(int dia, int mes,int Ano, string idProfissional)
        {
            dia = 23;
            mes = 2;
            Ano = 2018;
            idProfissional = "4FCE1A00-1496-415D-76FE-08D57246C8AB";

            Dia day = CalendarioRules.PreencheDia(dia, mes, Ano, sqlServices, contexto, idProfissional);

            return View(day);
        }
    }
}
