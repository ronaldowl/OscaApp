using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("CategoriaManutencao")]
    public class CategoriaManutencao
    {  
        public Guid id { get; set; }  
        public String nome { get; set; }        
    }
}
