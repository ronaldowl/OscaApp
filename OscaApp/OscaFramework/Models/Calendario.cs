using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    public class Calendario 
    {
        public Semana semana1 { get; set; }
        public Semana semana2 { get; set; }
        public Semana semana3 { get; set; }
        public Semana semana4 { get; set; }
        public Semana semana5 { get; set; }

        public List<Dia> dias { get; set; }

        public int mes { get; set; }
        public int ano { get; set; }
        public int qtdDias { get; set; }

        public string nomeMes { get; set; }


    }
}
