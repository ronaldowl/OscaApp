using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("OrdemServico")]
    public class OrdemServico : GenericEntity
    {
        public String codigo { get; set; }
        public int horaInicio { get; set; }
        public int horaFim { get; set; }
        public DateTime dataAgendada { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idCategoriaManutencao { get; set; }
        public string problema { get; set; }
        public string diagnostico { get; set; }
        public string laudo { get; set; }
        public string observacao { get; set; }

        public string marca { get; set; }
        public string modelo { get; set; }
        public string cor { get; set; }
        public string numeroSerie { get; set; }





        public CustomEnum.tipoOrdemServico tipo { get; set; }
        public CustomEnumStatus.StatusOrdemServico statusOrdemServico { get; set; }
        
        public OrdemServico()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 5;
        }
    }
}
