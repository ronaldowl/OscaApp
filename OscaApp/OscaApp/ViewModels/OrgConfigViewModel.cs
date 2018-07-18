using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class OrgConfigViewModel
    {
       public OrgConfig orgConfig { get; set; }
       public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
