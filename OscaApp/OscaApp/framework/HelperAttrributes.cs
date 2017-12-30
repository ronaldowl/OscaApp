using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework
{   

    /// <summary>
    /// Classe especializada em tratar campos que serão reenderizados
    /// </summary>
    public static class HelperAttributes
    {
        public static List<SelectListItem> PreencheDropDownList(List<Relacao> Entitys)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in Entitys)
            {
                items.Add(new SelectListItem { Text = item.idName, Value = item.id.ToString() });
            }
            
            return items;
        }
    }
}
