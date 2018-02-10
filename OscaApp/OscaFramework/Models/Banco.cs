using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("Banco")]
    public class Banco
    {  
        public Guid id { get; set; }
        public String codigo { get; set; }
        public String nome { get; set; }
        public String site { get; set; }      
        public CustomEnumStatus.Status status { get; set; }
    }
}
