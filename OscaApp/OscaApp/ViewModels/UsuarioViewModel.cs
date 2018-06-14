

using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class UsuarioViewModel
    {
        public ApplicationUser usuario { get; set; }
        public ContextPage contexto { get; set; }
        public string StatusMessage { get; set; }
 
        public List<SelectListItem> perfis { get; set; }
    }
}
