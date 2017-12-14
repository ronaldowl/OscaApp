using System;
using OscaApp.framework.Models;

namespace OscaApp.Models
{
    public class Fornecedor: GenericEntity
    {
        public String codigo { get; set; }
        public String nomeFornecedor { get; set; }
        public String cnpj { get; set; }

        public String nomeVendedor { get; set; }
        public String telefone { get; set; }
        public String email { get; set; }

        public String anotacao { get; set; }

        public CustomEnum.Status status { get; set; }

        public Fornecedor()
        {
            this.status = CustomEnum.Status.Ativo;
        }
    }
}
