using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("BalcaoVendas")]
    public class BalcaoVendas : GenericEntity
    {
        public Guid idOrganizacao { get; set; }   
        public Guid idListaPreco { get; set; }     
       
        //[DataType(DataType.Currency)]
        //[Column(TypeName = "money(5,2)")]
        //[Column(TypeName = "decimal(5, 2)")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorTotal { get; set; }       
       
        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
        public CustomEnum.tipoPagamento tipoPagamento { get; set; } 
        public CustomEnumStatus.StatusBalcaoVendas statusBalcaoVendas { get; set; }        

        public BalcaoVendas()
        {

            this.status = CustomEnumStatus.Status.Ativo;
            this.statusBalcaoVendas = CustomEnumStatus.StatusBalcaoVendas.EmAndamento;
            this.entityType = 31;            

        }

    }
}
