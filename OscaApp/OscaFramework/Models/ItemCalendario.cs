﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OscaFramework.Models.CustomEnum;

namespace OscaFramework.Models
{
    public class ItemCalendario
    {
        public string id       { get; set; }
        public string codigo { get; set; }
        public string cliente { get; set; }
        public string referencia { get; set; }
        public ItemHoraDia inicio { get; set; }
        public ItemHoraDia fim    { get; set; }
        public string titulo   { get; set; }
        public string tipo { get; set; }
        public CustomEnumStatus.StatusAgendamento statusAgendamento { get; set; }


    }
}
