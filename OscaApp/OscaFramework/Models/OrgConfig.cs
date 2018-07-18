using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("OrgConfig")]
    public class OrgConfig : GenericEntity
    {
       
        public Guid idOrganizacao { get; set; }

        public decimal margemBaseProduto { get; set; }

        public int qtdDiasCartaoCredito { get; set; }

        public int qtdDiasCartaoDebito { get; set; }

        public string mensagemPedido { get; set; }


        public OrgConfig()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 34;
        }
    }
}
