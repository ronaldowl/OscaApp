using System;
 

namespace OscaFramework.Models
{
    public class Agendamento : GenericEntity
    {
        public Agendamento()
        {
            this.entityType = 23;
            this.status = CustomEnumStatus.Status.Ativo;
        }   // end of ctor padrão
 
    } // end of class Agendamento
}
