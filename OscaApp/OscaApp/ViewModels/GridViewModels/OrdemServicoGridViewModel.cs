using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.ViewModels.GridViewModels
{
    public class OrdemServicoGridViewModel
    {
        public OrdemServico ordemServico { get; set; }
        public Relacao cliente { get; set; }
        public Relacao profissional { get; set; }

    }
}
