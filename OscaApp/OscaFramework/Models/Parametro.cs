using System;


namespace OscaFramework.Models
{
    public class Parametros :GenericEntity
    {
       
        //Propriedades locais

        public int codigo { get; set; }
        public  String valor { get; set; }
        public String Descrição { get; set; }

        public Parametros()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 8;
        }
    }
}
