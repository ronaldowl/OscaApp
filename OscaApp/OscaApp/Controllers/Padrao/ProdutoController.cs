using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using OscaFramework.MicroServices;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {

        private readonly ProdutoData produtoData;
        private readonly ProdutoFornecedorData produtoFornecedorData;

        private readonly ItemListaPrecoData itemListaPrecoData;
        private ContextPage contexto;
        private OscaConfig oscaConfig;
        private OrgConfigData  orgConfig;




        public ProdutoController(ContexDataService db, IHttpContextAccessor httpContext, OscaConfig _oscaConfig)
        {
            this.produtoData = new ProdutoData(db);
            this.itemListaPrecoData = new ItemListaPrecoData(db);
            this.produtoFornecedorData = new ProdutoFornecedorData(db);
            this.orgConfig = new OrgConfigData(db);
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.oscaConfig = _oscaConfig;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string idProdutoTemp { get; set; }


        [HttpGet]
        public ViewResult FormCreateProduto()
        {
            var orgParan = orgConfig.Get(this.contexto.idOrganizacao);

            ProdutoViewModel modelo = new ProdutoViewModel();
            modelo.produto = new Produto();
            modelo.contexto = contexto;
            modelo.produto.criadoEm = DateTime.Now;
            modelo.produto.criadoPorName = contexto.nomeUsuario;

            modelo.produto.margemLucroBase = orgParan.margemBaseProduto;
            modelo.produto.quantidadeMinima = orgParan.quantidadeMinimaProduto;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProduto(ProdutoViewModel entrada)
        {

            Produto prod = new Produto();
            Relacao lista = new Relacao();
            ItemListaPreco itemLista = new ItemListaPreco();
            SqlGenericData sqlService = new SqlGenericData();



            try
            {
                if (entrada.produto != null)
                {
                    if (ProdutoRules.MontaProdutoCreate(entrada, out prod, contexto))
                    {
                        produtoData.Add(prod);

                        //Create de item da lista se houver lista padrão
                        lista = sqlService.RetornaRelacaoListaPrecoPadrao(contexto.idOrganizacao);

                        if (lista.idName != null)
                        {
                            itemLista.idProduto = prod.id;
                            itemLista.idListaPreco = lista.id;
                            itemLista.valor = (prod.valorCompra / 100) * prod.margemLucroBase + prod.valorCompra;
                            itemLista.valorMinimo = itemLista.valor;
                            ItemListaPrecoRules.ItemListaPrecoCreateRelacionado(itemLista, contexto);
                            itemListaPrecoData.Add(itemLista);
                        }
                        return RedirectToAction("FormUpdateProduto", new { id = prod.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 7, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProduto-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateProduto(string id)
        {
            ProdutoViewModel modelo = new ProdutoViewModel();
            modelo.produto = new Produto();
            modelo.produto.id = new Guid(id);

            Produto retorno = new Produto();

            if (!String.IsNullOrEmpty(id))
            {

                retorno = produtoData.Get(modelo.produto.id);
                modelo.itensListaPreco = new List<ItemProdutoLista>();
                modelo.itensListaPreco = ProdutoRules.RetornaItemListaProduto(itemListaPrecoData.GetAllByProduto(modelo.produto.id));
                modelo.produtoFornecedor = produtoFornecedorData.GetAllByProduto(retorno.id);
                retorno.urlProduto = "http://" + retorno.urlProduto;

                //apresenta mensagem de registro atualizado com sucesso
                modelo.StatusMessage = StatusMessage;


                if (retorno != null)
                {
                    modelo.produto = retorno;
                }
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateProduto(ProdutoViewModel entrada)
        {
            Produto modelo = new Produto();
            entrada.contexto = this.contexto;


            try
            {
                if (ProdutoRules.MontaProdutoUpdate(entrada, out modelo))
                {
                    produtoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 7, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProduto-post", ex.Message);
            }

            return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString() });
        }

        public ViewResult GridProduto(string filtro, int Page)
        {
            IEnumerable<Produto> retorno = produtoData.GetAll(contexto.idOrganizacao);


            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.nome.ToLower().Contains(filtro.ToLower())) ||
                                (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))||
                                (u.codigoBarra == filtro)
                          select u;

              

            }

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Produto>(Page, 100));
        }
        public ViewResult LookupProduto(string filtro, int Page)
        {
            IEnumerable<Produto> retorno = produtoData.GetAll(contexto.idOrganizacao);


            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.nome.ToLower().Contains(filtro.ToLower())) ||
                                (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))||
                                 (u.codigoBarra == filtro)
                          select u;
            }

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Produto>(Page, 20));
        }
        [HttpGet]
        public ViewResult AddImage(string idProduto)
        {
            idProdutoTemp = idProduto;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            string path = "";
            string URLPAT = "";
            string NomeFile = Path.GetFileName(file.FileName);

            //Regra 1 
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //Regra 2 - Remove espaço
             if(  NomeFile.Contains(' '))
                return Content("Não pode haver espaço em branco no nome do arquivo");

            //Regra 3 - Limite de tamanho
            if (file.Length >= 2001000)
                return Content("Não pode ter mais de 2 MB");

            //Regra 4 - Apenas PNG e JPG
            string[] contentTypes = new string[] { "image/jpg", "image/png", "image/jpeg" };
            if (!contentTypes.Contains(file.ContentType))
            {
                return Content("Suporte apenas para arquivos PNG,JPG e JPEG");
            }


            if (this.oscaConfig.ambiente == "prod")
            {
                path = "h:\\root\\home\\ronaldowl-001\\www\\bancoimagem\\prod\\OrgFiles\\" + this.contexto.organizacao + "\\produto\\imagens\\";
                URLPAT = "imagens.oscas.com.br/prod/orgfiles/" + this.contexto.organizacao + "/produto/imagens/" + NomeFile;

            }
            else
            {
                path = "h:\\root\\home\\ronaldowl-001\\www\\bancoimagem\\desenv\\OrgFiles\\" + this.contexto.organizacao + "\\produto\\imagens\\";
                URLPAT = "imagens.oscas.com.br/desenv/orgfiles/" + this.contexto.organizacao + "/produto/imagens/" + NomeFile;
            }

            path = path + NomeFile ;

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Produto modelo = new Produto();
            ProdutoViewModel entrada = new ProdutoViewModel();
            entrada.contexto = this.contexto;
            entrada.produto.id = new Guid(idProdutoTemp);
            entrada.produto.urlProduto = URLPAT;
            
            if (ProdutoRules.MontaProdutoUpdate(entrada, out modelo))
            {
                produtoData.UpdateImage(modelo);                
            }

            return RedirectToAction("FormUpdateProduto", new { id = entrada.produto.id });

        }
    }
}