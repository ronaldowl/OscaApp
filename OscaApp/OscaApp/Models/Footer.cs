using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OscaApp.framework.Models.CustomEnum;

namespace OscaApp.Models
{
    public class Footer
    {

        public string nomeOrganizacao { get; set; }
        public string msgAvaliacao { get; set; }
        public int diasAvaliacao { get; set; }
        public StatusOrganizacao statusOrg { get; set; }

    }
}
