using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class AtendimentoRules
    {
        public static bool AtendimentoCreate(AtendimentoViewModel entrada,out Atendimento modelo, ContextPage contexto )
        {
            modelo = new Atendimento();
            modelo = entrada.atendimento;
            modelo.status = CustomEnumStatus.Status.Ativo;
            modelo.codigo = AutoNumber.GeraCodigo(3, contexto.idOrganizacao);
            modelo.idCliente = entrada.cliente.id;
                        
            if (entrada.servico != null) modelo.idReferencia = entrada.servico.id;
            if (entrada.os != null) modelo.idReferencia = entrada.os.id;

            if (entrada.horaInicio != null) modelo.horaInicio = Convert.ToInt32(entrada.horaInicio.horaDia);
            if (entrada.horaFim != null) modelo.horaFim = Convert.ToInt32(entrada.horaFim.horaDia);
            
            if (modelo.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool AtendimentoUpdate(AtendimentoViewModel entrada,out Atendimento modelo)
        {
            modelo = new Atendimento();
            modelo = entrada.atendimento;

            if (entrada.cliente != null) modelo.idCliente = entrada.cliente.id;

            if (entrada.atendimento.tipoReferencia == CustomEnum.TipoReferencia.servico)
            {
                if (entrada.servico != null) modelo.idReferencia = entrada.servico.id;

            }

            if (entrada.atendimento.tipoReferencia == CustomEnum.TipoReferencia.OrdemServico)
            {
                if (entrada.os != null) modelo.idReferencia = entrada.os.id;
              
            }
          

            if (entrada.horaInicio != null) modelo.horaInicio = Convert.ToInt32(entrada.horaInicio.horaDia);
            if (entrada.horaFim != null) modelo.horaFim = Convert.ToInt32(entrada.horaFim.horaDia);
            
            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
}
}
