using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class ItemProdutoLista
    { 
        public Guid id { get; set; }      
        public string nomeListaPreco { get; set; }
        public Guid idListaPreco { get; set; }
        public decimal valorVenda { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
