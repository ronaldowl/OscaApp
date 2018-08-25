using System;
using System.Collections.Generic;
using System.Text;

namespace OscaFramework.Models
{
    public class LogAcesso
    {
        public string id  {get;set; }
        public string idUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string idOrganizacao { get; set; }
        public string dataLogin { get; set; }
    }
}
