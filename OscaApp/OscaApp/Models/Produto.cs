using System;
using OscaApp.framework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaApp.Models
{
    [Table("produto")]
    public class Produto : GenericEntity
    {
        public string codigo { get; set; }
        public string codigoFabricante { get; set; }
        public string fabricante { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal valorCompra { get; set; }
        public int quantidade { get; set; }
        public decimal largura { get; set; }
        public decimal altura { get; set; }
        public decimal area { get; set; }
        public decimal peso { get; set; }
        public CustomEnum.FormaVendaProduto formaVendaProduto { get; set; }
        public Guid idOrganizacao { get; set; }


        public Produto()
        {
            this.status = CustomEnum.Status.Ativo;
        }

    }

  
}
