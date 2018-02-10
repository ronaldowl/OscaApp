using OscaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Services
{
    public interface IContextPage
    {
         ContextPage contexto { get; set; }
         ContextPage RetornaContextoServices();
    }
}
