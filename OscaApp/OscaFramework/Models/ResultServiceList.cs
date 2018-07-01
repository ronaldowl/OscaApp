using System;
using System.Collections.Generic;

namespace OscaFramework.Models
{
    /// <summary>
    /// Classe usada para controle de retorno dos serviços API
    /// </summary>
    public class ResultServiceList
    {
        public  List<ProdutoBalcao> ListaProdutoBalcao   { get; set; }
        public bool statusOperation {get;set;}
        public string statusMensagem { get; set; }
    }
}
