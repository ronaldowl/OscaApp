using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("BalcaoVendas")]
    public class BalcaoVendas : GenericEntity
    {

        public string codigo { get; set; }
        public string cpf { get; set; }
        public int parcelas { get; set; }
        public int diaVencimento { get; set; }

        public Guid idOrganizacao { get; set; }   
        public Guid idListaPreco { get; set; }
        public Guid idCliente { get; set; }
            
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorTotal { get; set; }
               
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal troco { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDinheiroPago { get; set; }

        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
        public CustomEnum.tipoPagamento tipoPagamento { get; set; } 
        public CustomEnumStatus.StatusBalcaoVendas statusBalcaoVendas { get; set; }    
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }

        public BalcaoVendas()
        {

            this.status = CustomEnumStatus.Status.Ativo;
            this.statusBalcaoVendas = CustomEnumStatus.StatusBalcaoVendas.EmAndamento;
            this.tipoDesconto = CustomEnum.tipoDesconto.Money;
            this.entityType = 31;            

        }

    }
}
