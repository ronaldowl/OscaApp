using System;
 

namespace OscaFramework.Models
{
    /// <summary>
    /// Classe usada para controle de retorno dos serviços API
    /// </summary>
    public class ResultService
    {       
        public object value { get; set; }
        public bool statusOperation {get;set;}
        public string statusMensagem { get; set; }
    }
}
