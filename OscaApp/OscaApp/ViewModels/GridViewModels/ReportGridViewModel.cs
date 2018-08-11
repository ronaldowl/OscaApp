using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class ReportGridViewModel
    {
        public List<Report> reportsNativo { get; set; }
        public List<Report> reportsCustomizado { get; set; }
    }
}
