﻿using System;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using OscaApp.framework;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OscaApp.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        
        private readonly IPedidoData pedidoData;
        private readonly IListaPrecoData listaprecoData;
        private readonly SqlGenericData Sqlservice;
        private ContextPage contexto;


        public PedidoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
             
            this.pedidoData = new PedidoData(db);
            this.listaprecoData = new ListaPrecoData(db);
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.Sqlservice = new SqlGenericData();
        }

        [HttpGet]
        public ViewResult FormCreatePedido(string idCliente)
        {
            PedidoViewModel modelo = new PedidoViewModel();

            try
            {
                modelo.contexto = contexto;
                modelo.pedido.criadoEm = DateTime.Now;
                modelo.pedido.criadoPorName = contexto.nomeUsuario;

                //Se passar o id carrega o cliente
                if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = Sqlservice.RetornaCliente(new Guid(idCliente));

                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
                modelo.listaPrecos = itens;

            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-get", ex.Message);
            }
          
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreatePedido(PedidoViewModel entrada)
        {
            Pedido pedido = new Pedido();
            try
            {
                if (PedidoRules.PedidoCreate(entrada, out pedido, contexto))
                {
                    pedidoData.Add(pedido);
                    return RedirectToAction("FormUpdateItemListaPreco", new { id = pedido.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-post", ex.Message);
            }
            return View();
        }

       [HttpGet]
        public ViewResult FormUpdatePedido(string id)
        {
            PedidoViewModel modelo = new PedidoViewModel();

            try           
           {
                Pedido retorno = new Pedido();
          
               if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = pedidoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.pedido = retorno;

                        //Preenche informações do grid de Endereço
                       // modelo.enderecos = enderecoData.GetByCliente(modelo.cliente.id);

                   }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-get", ex.Message);

             }

            return View(modelo);
        }


        public ViewResult GridPedido(string filtro, int Page)
        {
            try
            {
                IEnumerable<Pedido> retorno = pedidoData.GetAll(contexto.idOrganizacao);

                if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigoPedido == filtro) select A;

                retorno = retorno.OrderBy(x => x.codigoPedido);

                //Se não passar a número da página, caregar a primeira
                if (Page == 0) Page = 1;
                return View(retorno.ToPagedList<Pedido>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,4, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }

        //[HttpPost]
        //public IActionResult FormUpdateCliente(ClienteViewModel entrada)
        //{
        //    Cliente modelo = new Cliente();
        //    entrada.contexto = this.contexto;
        //    try
        //    {
        //        if (ClienteRules.MontaClienteUpdate(entrada, out modelo))
        //        {
        //            clienteData.Update(modelo);
        //            return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogOsca log = new LogOsca();
        //        log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-post", ex.Message);
        //    }

        //    return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
        //}
      

        //[HttpPost]
        //public IActionResult FormCreatePedido(ClienteViewModel entrada)
        //{

        //    Cliente modelo = new Cliente();
        //    entrada.contexto = contexto;

        //    try
        //    {
        //        if (entrada.cliente.nomeCliente != null)
        //        {
        //            if (ClienteRules.MontaClienteCreate(entrada, out modelo, contexto))
        //            {
        //                clienteData.Add(modelo);

        //                return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogOsca log = new LogOsca();
        //        log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateCliente", ex.Message);

        //    }
        //    return View();
        //}

    }
}
