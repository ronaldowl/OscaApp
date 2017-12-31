using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class ItemListaPreco : GenericEntity
    {
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public Guid idProduto { get; set; }
        public string idProdutoName { get; set; }
        public Guid idListaPreco { get; set; }
        public string idListaPrecoName { get; set; }
        public Guid idOrganizacao { get; set; }


        public ItemListaPreco()
        {
            this.status = CustomEnum.Status.Ativo;
            this.entityType = 13;
        }
    }
}
