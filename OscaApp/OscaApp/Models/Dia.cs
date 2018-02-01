using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class Dia
    {
        public List<OrdemServico> ordensServico { get; set; }
        public int dia { get; set; }
        public string nome { get; set; }

    }
}
