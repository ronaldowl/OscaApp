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

        public ContextPageServices( )
        { 
        }

        public ContextPageServices(string Email, string Org)
        {
            this.contexto = new ContextPage(Email, Org);
        }

        public void RetornaContexto(string Email, string Org)
        {
            this.contexto  = new ContextPage(Email, Org);
        }

        public ContextPage RetornaContextoServices( )
        {
            return this.contexto;
        }
    }
}
