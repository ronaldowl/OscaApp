using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class AtividadeGridViewModel
    {
        public Atividade atividade { get; set; }      
        public Relacao profissional { get; set; }
    }
}
