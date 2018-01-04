using System;
using OscaApp.framework.Models;
using OscaApp.framework;


namespace OscaApp.Models
{
    public class Parametros : GenericEntity
    {
       
        //Propriedades locais

        public int codigo { get; set; }
        public  String valor { get; set; }
        public String Descrição { get; set; }
        public CustomEnum.TipoParametro tipo { get; set; }
        public CustomEnumStatus.Status status { get; set; }

    }
}
