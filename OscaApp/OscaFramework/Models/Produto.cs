using System;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("produto")]
    public class Produto : GenericEntity
    {
        public string codigo { get; set; }
        public string codigoFabricante { get; set; }
        public string fabricante { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string urlProduto { get; set; }
        public string codigoBarra { get; set; }


        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorCompra { get; set; }

        public int quantidade { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal margemLucroBase { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal largura { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal altura { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal area { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal peso { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal icms { get; set; }

        
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal ipi { get; set; }

       
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal iss { get; set; }

        public CustomEnum.FormaVendaProduto formaVendaProduto { get; set; }

        public CustomEnum.TipoProduto tipoProduto { get; set; }
        
        public Guid idOrganizacao { get; set; }


        public Produto()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 7;
            this.formaVendaProduto = CustomEnum.FormaVendaProduto.Unidade;
        }

    }

  
}
