using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels
{
    public class GridClienteViewModel
    {
        public X.PagedList.PagedList<Cliente> Clientes { get; set; }
    }
}
