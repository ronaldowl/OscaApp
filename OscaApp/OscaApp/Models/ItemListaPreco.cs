using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class ItemListaPreco : GenericEntity
    {
        public decimal valor { get; set; }
        public Guid produto { get; set; }
        public Guid listaPreco { get; set; }
        public Guid idOrganizacao { get; set; }
    }
}
