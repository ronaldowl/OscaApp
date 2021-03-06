﻿ 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OscaFramework.Data;
using OscaFramework.MicroServices;

namespace OscaFramework.Models
{
    [Table("Organizacao")]
    public class Organizacao : GenericEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "O {0} nome da empresa deve ter no minimo  {2} e no máximo {1} characters de tamanho", MinimumLength = 3)]

        public string nomeLogin { get; set; }
        public string nomeAmigavel { get; set; }
        public string cpf_cnpj { get; set; }
        
        public CustomEnum.TipoPessoa tipoPessoa { get; set; }       
        public int quantidadeUsuario { get; set; }

        public int servicoPaginaCapturaLead { get; set; }

        public string nomeAdministrador { get; set; }
        public string emailAdministrador { get; set; }
        public string telefoneAdministrador { get; set; }
        public string celularAdministrador { get; set; }
        public string cargo { get; set; }
        public string logradouro { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string pais { get; set; }
        public string cep { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string anotacao { get; set; }
        public string emailEmpresa { get; set; }
        public string TelefoneEmpresa { get; set; }


        public CustomEnumStatus.StatusOrg statusOrg { get; set; }        
        public DateTime dataExpiracao { get; set; }      
        public DateTime dataPagamento { get; set; }

        [Key]
        public Guid id { get; set; }

        [NotMapped]
        public int entityType { get; set; }
        public Guid criadoPor { get; set; }
        public string criadoPorName { get; set; }
        public Guid modificadoPor { get; set; }
        public string modificadoPorName { get; set; }
        public CustomEnumStatus.Status status { get; set; }

        public DateTime modificadoEm { get; set; }
        public DateTime criadoEm { get; set; }


        public Organizacao(Guid idOrg)
        {
            ////Inicia objeto Org com os campos preenchidos
            SqlGenericManager sqlData = new SqlGenericManager();
            var org = sqlData.RetornaOrganizacao(idOrg);

            this.nomeAmigavel = org.nomeAmigavel;
            this.statusOrg = org.statusOrg;
            this.dataExpiracao = org.dataExpiracao;
            this.id = org.id;
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 1000;
        }

        //Contrutor padrão
        public Organizacao()
        {

            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 1000;

        }
    }
}
