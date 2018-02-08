using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.Models;

namespace OscaApp.ViewModels.GridViewModels
{
    public class ClienteGridViewModel
    {
        public X.PagedList.PagedList<Cliente> Clientes { get; set; }
    }
}
