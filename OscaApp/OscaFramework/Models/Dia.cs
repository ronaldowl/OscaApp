using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    public class Dia
    {
        public List<ItemCalendario> itensCalendario { get; set; }
        public List<ItemCalendario> itensCalendarioVazios { get; set; }

        public int dia { get; set; }
        public int ano { get; set; }
        public string nomeDia { get; set; }
        public string nomeMes { get; set; }


    }
}
