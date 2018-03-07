using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    public class ItemHoraDia
    {         
        [Key]
        public Guid id { get; set; }

        public CustomEnum.itemHora horaDia { get; set; }
        public string HoraFormatada
        {
             get; set; 
        }
    }
}
