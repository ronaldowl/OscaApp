using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using OscaFramework.Models;

namespace OscaApp.Models
{
    public class Footer
    {

        public string nomeOrganizacao { get; set; }
        public string msgAvaliacao { get; set; }
        public int diasAvaliacao { get; set; }
        public CustomEnumStatus.StatusOrg statusOrg { get; set; }

    }
}
