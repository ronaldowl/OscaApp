using System;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{   
    public class ProdutoBalcao 
    {
        public Guid id { get; set; }

        public int entityType { get; set; }
        
        public string codigo { get; set; }
        
        public string nome { get; set; }
        
        public string fabricante { get; set; }
           
        public string modelo { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorTotal { get; set; }

        public int quantidade { get; set; }            
        public Guid idOrganizacao { get; set; }
        public Guid idBalcaoVenda { get; set; }
        public Guid idListaPreco { get; set; }
        public Guid idItemListaPreco { get; set; }
        
        public ProdutoBalcao()
        {
         
            this.entityType = 32;
       
        }

    }

  
}
