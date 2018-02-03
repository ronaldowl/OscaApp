﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OscaApp.framework.Models.CustomEnum;

namespace OscaApp.Models
{
    public class ItemCalendario
    {
        public string id       { get; set; }
        public string inicio { get; set; }
        public string fim    { get; set; }
        public string titulo   { get; set; }
        public tipoItemCaledario tipo { get; set; }


    }
}
