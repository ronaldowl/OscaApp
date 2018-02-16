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
    public static class ServicoOrdemRules
    {
        public static bool ServicoOrdemCreate(ServicoOrdemViewModel entrada,out ServicoOrdem modelo, ContextPage contexto )
        {
            modelo = new ServicoOrdem();
            modelo = entrada.servicoOrdem;
            modelo.idServico = entrada.servico.id;
            modelo.idOrdemServico = entrada.ordemServico.id;
            modelo.status = CustomEnumStatus.Status.Ativo;      
                     
            if (modelo.idOrganizacao != null)
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
        public static bool ServicoOrdemUpdate(ServicoOrdemViewModel entrada,out ServicoOrdem modelo)
        {
            modelo = new ServicoOrdem();
            modelo = entrada.servicoOrdem;
            modelo.idServico = entrada.servico.id;

            //************ Objetos de controle de acesso *******************           
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
}
}
