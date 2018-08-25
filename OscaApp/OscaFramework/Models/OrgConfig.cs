using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("OrgConfig")]
    public class OrgConfig : GenericEntity
    {
       
        public Guid idOrganizacao { get; set; }

        //Sessão Produto
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal margemBaseProduto { get; set; }

        public int quantidadeMinimaProduto { get; set; }

        //Sessão Contas Receber
        public int qtdDiasCartaoCredito { get; set; }

        public int qtdDiasCartaoDebito { get; set; }

        //Sessão Impressão Cupom        
        public string mensagemCupom { get; set; }        
        public string cupom_altura { get; set; }
        public string cupom_largura { get; set; }
        public string cupom_fontesize { get; set; }

        //Sessão PedidoRetirada
        public string mensagemPedido { get; set; }
        public string tituloImpressao { get; set; }

        public OrgConfig()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 34;
        }
    }
}
