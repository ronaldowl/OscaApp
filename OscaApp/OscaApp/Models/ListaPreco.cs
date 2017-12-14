using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class ListaPreco : GenericEntity
    {
        public string nome { get; set; }
        public DateTime dataValidade { get; set; }

    }
}
