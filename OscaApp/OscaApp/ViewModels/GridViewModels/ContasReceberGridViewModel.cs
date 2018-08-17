using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.MicroServices;

namespace OscaApp.ViewModels.GridViewModels
{
    public class ContasReceberGridViewModel
    {
        public ContasReceber contasReceber { get; set; }      
        public Cliente cliente { get; set; }
        public SqlGenericData sqlservice  { get; set;  }

        public ContasReceberGridViewModel()
        {
            this.sqlservice = new SqlGenericData();
        }
    }
}
