using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OscaFramework.Models.CustomEnum;

namespace OscaFramework.Models
{
    public class ItemCalendario
    {
        public string id       { get; set; }
        public ItemHoraDia inicio { get; set; }
        public ItemHoraDia fim    { get; set; }
        public string titulo   { get; set; }
        public tipoItemCaledario tipo { get; set; }
        public CustomEnumStatus.StatusAtendimento statusAtendimento { get; set; }


    }
}
