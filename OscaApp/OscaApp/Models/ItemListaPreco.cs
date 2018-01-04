using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{

    [Table("itemlistapreco")]
    public class ItemListaPreco : GenericEntity
    {
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public Guid idProduto { get; set; } 
        public Guid idListaPreco { get; set; }    
        public Guid idOrganizacao { get; set; }


        public ItemListaPreco()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 13;
        }
    }
}
