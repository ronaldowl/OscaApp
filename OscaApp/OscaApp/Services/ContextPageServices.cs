using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaApp.Data;

namespace OscaApp.Services
{
    public class ContextPageServices : IContextPage
    {
        public ContextPage contexto { get; set; }

        public void RetornaContexto(string Email, string Org)
        {
            this.contexto  = new ContextPage(Email, Org);
        }
    }
}
