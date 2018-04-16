using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class AgendamentoGridViewModel
    {
        public Agendamento agendamento { get; set; }      
        public Cliente cliente { get; set; }
        public String referencia { get; set; }
        public ItemHoraDia horaInicio { get; set; }
        public ItemHoraDia horaFim { get; set; }

    }
}
