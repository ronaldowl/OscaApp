 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    [Table("PerfilAcesso")]
    public class PerfilAcesso : GenericEntity
    {
        public string nome                          { get; set; }         
        public bool permitePainelVendas             { get; set; }
        public bool permitePainelServico            { get; set; }
        public bool permitePainelFinanceiro         { get; set; }
        public bool permitePainelCadastro           { get; set; }
        public bool permitePainelConfiguracoes      { get; set; }
        public bool permitePainelHome               { get; set; }
        public bool permitePainelGlobal             { get; set; }
        public bool permitePainelSuporte            { get; set; }
        public bool permitePainelGerenciamento      { get; set; }
        public bool permitePainelOperacional        { get; set; }

        public bool permiteTodosAtendimento         { get; set; }
        public bool permiteTodosAtividades          { get; set; }
        public bool permiteTodosOrdemServico        { get; set; }
        public bool permiteTodosAgendamentos        { get; set; }



        public Guid idOrganizacao { get; set; }

        public PerfilAcesso()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 29;
        }

    }
}
