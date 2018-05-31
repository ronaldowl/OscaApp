using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers 
{
    [Authorize]
    public class VolumetriaController : Controller
    {
        private readonly SqlGenericData sqlData;
        private ContextPage contexto;

        public VolumetriaController(SqlGenericData _sqlData, IHttpContextAccessor httpContext)
        {
            this.sqlData =  _sqlData;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }
     
        public ViewResult VolumetriaResumo( )
        {
            Volumetria modelo = new Volumetria();
            modelo = sqlData.RetornaVolumetriaResumo(contexto.idOrganizacao);
            return View(modelo);
        }        
    }
}
