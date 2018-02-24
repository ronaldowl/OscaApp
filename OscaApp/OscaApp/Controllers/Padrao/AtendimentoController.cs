using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using Microsoft.AspNetCore.Http;
using OscaApp.RulesServices;
using OscaApp.framework;
using OscaFramework.MicroServices;
using X.PagedList;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    public class AtendimentoController : Controller
    {
        private ContextPage contexto;
        public AtendimentoData atendimentoData;
        private readonly SqlGenericDataServices sqlData;

        public AtendimentoController(ContexDataService db, IHttpContextAccessor httpContext,  SqlGenericDataServices _sqlData)
        {
            this.atendimentoData = new AtendimentoData(db);
            this.sqlData = _sqlData;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateAtendimento(string id)
        {
            AtendimentoViewModel modelo = new AtendimentoViewModel();
            modelo.contexto = contexto;
            modelo.atendimento = new Atendimento();
            modelo.atendimento.status = CustomEnumStatus.Status.Ativo;

            modelo.atendimento.criadoEm = DateTime.Now;
            modelo.atendimento.criadoPorName = contexto.nomeUsuario;       
            
            return View(modelo);
        }

        [HttpPost]
        public ActionResult FormCreateAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento  modelo = new Atendimento ();

            if(entrada.atendimento != null)
            {

               if( AtendimentoRules.AtendimentoCreate(entrada,out modelo, this.contexto))
                {
                    //Se retorna true grava no banco
                    atendimentoData.Add(modelo);

                    return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
                }
            }                

            return View( );
        }

        [HttpGet]
        public ViewResult FormUpdateAtendimento(string id)
        {
            AtendimentoViewModel modelo = new AtendimentoViewModel();

            try
            {
                Atendimento retorno = new Atendimento();
              
                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = atendimentoData.Get(new Guid(id));

                    modelo.cliente = sqlData.RetornaRelacaoCliente(retorno.idCliente);
                    modelo.servico = sqlData.RetornaRelacaoServico(retorno.idServico);
                    modelo.horaInicio = new ItemHoraDia();
                    modelo.horaInicio.horaDia = (CustomEnum.itemHora)retorno.horaInicio;
                    modelo.horaFim = new ItemHoraDia();
                    modelo.horaFim.horaDia = (CustomEnum.itemHora)retorno.horaFim;

                    if (retorno != null)
                    {
                        modelo.atendimento = retorno;                     
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento modelo = new Atendimento();
            entrada.contexto = this.contexto;
            try
            {
                if (AtendimentoRules.AtendimentoUpdate(entrada, out modelo))
                {
                    atendimentoData.Update(modelo);
                    return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-post", ex.Message);
            }

            return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
        }

        public ViewResult GridAtendimento(string filtro, int Page)
        {
            IEnumerable<AtendimentoGridViewModel> retorno = atendimentoData.GetAllGridViewModel(contexto.idOrganizacao);
                   
            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.atendimento.codigo == filtro  ) select A;

          
            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<AtendimentoGridViewModel>(Page, 10));
        }
    }
}
