using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    public class ItemProdutoLista
    { 
        public Guid id { get; set; }      
        public string nomeListaPreco { get; set; }
        public Guid idListaPreco { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public string valorVenda { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
