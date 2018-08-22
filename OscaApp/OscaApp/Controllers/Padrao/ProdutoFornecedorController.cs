using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.Models;
using System;
using System.Collections.Generic;

namespace OscaApp.Controllers
{
    public class ProdutoFornecedorController : Controller
    {

        private readonly IProdutoData produtoData;
        private readonly IProdutoFornecedorData produtoFornecedorData;    
        private readonly IFornecedorData fornecedorData;


        private ContextPage contexto;

        public  ProdutoFornecedorController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);          
            this.produtoFornecedorData = new ProdutoFornecedorData(db);   
            this.fornecedorData = new FornecedorData(db);

            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateProdutoFornecedor(string idProduto)
        {
            ProdutoFornecedorViewModel modelo = new ProdutoFornecedorViewModel();

            try
            {
                modelo.contexto = contexto;
                modelo.produto = produtoData.GetRelacao(new Guid(idProduto));

                modelo.produtoFornecedor.criadoEm = DateTime.Now;
                modelo.produtoFornecedor.criadoPorName = contexto.nomeUsuario;        

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoFornecedor-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProdutoFornecedor(ProdutoFornecedorViewModel entrada)
        {
            ProdutoFornecedor produtoFornecedor = new ProdutoFornecedor();
            try
            {
                if (entrada.produtoFornecedor != null)
                {
                    if (ProdutoFornecedorRules.ProdutoFornecedorCreate(entrada, out produtoFornecedor, contexto))
                    {
                        produtoFornecedorData.Add(produtoFornecedor);
                        return RedirectToAction("FormUpdateProdutoFornecedor", new { id = produtoFornecedor.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoFornecedor-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateProdutoFornecedor(string id)
        {
            ProdutoFornecedorViewModel modelo = new ProdutoFornecedorViewModel();
            
            try
            {
                ProdutoFornecedor retorno = new ProdutoFornecedor();
                modelo.contexto = this.contexto;

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = produtoFornecedorData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.produtoFornecedor = retorno;
                 
                        //Preenche produto e Fornecedor
                        modelo.produto = produtoData.GetRelacao(modelo.produtoFornecedor.idProduto);

                        Fornecedor fornecedor = fornecedorData.Get(modelo.produtoFornecedor.idFornecedor);
                        modelo.fornecedor = new Relacao();
                        modelo.fornecedor.id = fornecedor.id;
                        modelo.fornecedor.idName = fornecedor.nomeFornecedor;
                        
                        //apresenta mensagem de registro atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoFornecedor-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateProdutoFornecedor(ProdutoFornecedorViewModel entrada)
        {
            ProdutoFornecedor produtoFornecedor = new ProdutoFornecedor();
            entrada.contexto = this.contexto;

            try
            {
                if (ProdutoFornecedorRules.ProdutoFornecedorUpdate(entrada, out produtoFornecedor))
                {
                    produtoFornecedorData.Update(produtoFornecedor);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateProdutoFornecedor", new { id = produtoFornecedor.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoFornecedor-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridProdutoFornecedor(string idProduto)
        {
            List<ProdutoFornecedorGridViewModel> modelo = produtoFornecedorData.GetAllByProduto(new Guid(idProduto));

            ViewBag.idProduto = idProduto;
            ViewBag.nomeProduto = produtoData.Get(new Guid(idProduto)).nome;

            return View(modelo);
        }

        public ViewResult GridFornecedorProduto(string idFornecedor)
        {
            List<ProdutoFornecedorGridViewModel> modelo = produtoFornecedorData.GetAllByFornecedor(new Guid(idFornecedor));

            ViewBag.idProduto = idFornecedor;
            ViewBag.nomeFornecedor = fornecedorData.Get(new Guid(idFornecedor)).nomeFornecedor;

            return View(modelo);
        }


    }
}
