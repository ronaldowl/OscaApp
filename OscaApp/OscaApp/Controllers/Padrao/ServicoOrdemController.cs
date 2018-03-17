using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    public class ServicoOrdemController : Controller
    {

        private readonly IServicoOrdemData servicoOrdemData;
        private readonly IServicoData servicoData; 
        private ContextPage contexto;

        public ServicoOrdemController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.servicoData = new ServicoData(db);
            this.servicoOrdemData = new ServicoOrdemData(db);     
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateServicoOrdem(string id )
        {
            ServicoOrdemViewModel modelo = new ServicoOrdemViewModel();
           
            try
            {
                modelo.contexto = contexto;
                modelo.servicoOrdem = new ServicoOrdem();
                modelo.ordemServico.id = new Guid(id);                  
              
                modelo.servicoOrdem.criadoEm = DateTime.Now;
                modelo.servicoOrdem.criadoPorName = contexto.nomeUsuario;

                IEnumerable<Servico> retorno = servicoData.GetAll(contexto.idOrganizacao);

                modelo.servicos = retorno.ToPagedList<Servico>(1, 5);


            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServicoOrdem-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateServicoOrdem(ServicoOrdemViewModel entrada)
        {
            ServicoOrdem modelo = new ServicoOrdem();
            try
            {
                if (entrada.servicoOrdem != null)
                {
                    if (ServicoOrdemRules.ServicoOrdemCreate(entrada, out modelo, contexto))
                    {
                        SqlGenericData sqlData = new SqlGenericData();
                        servicoOrdemData.Add(modelo);
                        return RedirectToAction("FormUpdateOrdemServico", "OrdemServico", new { id = entrada.ordemServico.id });

                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServicoOrdem-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult FormUpdateServicoOrdem(string id)
        {
            ServicoOrdemViewModel modelo = new ServicoOrdemViewModel();
            SqlGenericData sqlData = new SqlGenericData();

            try
            {
                modelo.servicoOrdem = servicoOrdemData.Get(new Guid(id));
                modelo.servico = new Relacao();
                modelo.ordemServico = new Relacao();
                modelo.ordemServico = sqlData.RetornaRelacaoOrdemServicoPorIDServicoOrdem(new Guid(id));

                modelo.servico = sqlData.RetornaRelacaoServico(modelo.servicoOrdem.idServico);
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateServicoOrdem(ServicoOrdemViewModel entrada)
        {
            ServicoOrdem modelo = new ServicoOrdem();             

            try
            {
                if (ServicoOrdemRules.ServicoOrdemUpdate(entrada, out modelo, this.contexto))
                {
                    servicoOrdemData.Update(modelo);
                    return RedirectToAction("FormUpdateOrdemServico", "OrdemServico", new { id = entrada.ordemServico.id });

                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridServicoOrdem(string id)
        {
            IEnumerable<ServicoOrdemGridViewModel> retorno = servicoOrdemData.GetAllGridViewModel(new Guid(id));

            return View(retorno.ToPagedList<ServicoOrdemGridViewModel>(1, 10));
        }

        public IActionResult DeleteServicoOrdem(string id, string idOrdem)
        {
            ServicoOrdem modelo = new ServicoOrdem();
            modelo.id = new Guid(id);
            servicoOrdemData.Delete(modelo);
            return RedirectToAction("GridServicoOrdem", new { id = idOrdem });
        }
    }
}