using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    
    public class EnderecoCorreio 
    {
      
        //Propriedades locais
        public String logradouro { get; set; }
        public String cep { get; set; }
        public String cidade { get; set; }
        public String bairro { get; set; }

        public String numero { get; set; }
        public String complemento { get; set; }    
    
        public string estado { get; set; }
  

       
    }
}
