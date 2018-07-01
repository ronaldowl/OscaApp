using System;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("produtoBalcao")]
    public class ProdutoBalcao : GenericEntity
    {
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

        public ProdutoBalcao()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 32;
       
        }

    }

  
}
