using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("CategoriaProfissional")]
    public class CategoriaProfissional
    {  
        public Guid id { get; set; }
        public String nome { get; set; }     
    }
}