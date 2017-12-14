using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public static class EnderecoRules
    {
        public static bool MontaEnderecoCreate(EnderecoViewModel entrada,out Endereco modelo, ContextPage contexto )
        {
            modelo = new Endereco ();        
            
            if (entrada.endereco != null)
            {
                modelo = entrada.endereco;

                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;             
                modelo.idOrganizacao = contexto.idOrganizacao;
             

                return true;
            }           
            
            return false;
        }
        public static bool MontaEnderecoUpdate(EnderecoViewModel entrada, out Endereco modelo)
        {
            modelo = new Endereco();

            modelo = entrada.endereco;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;

            return true;
           
        }

          
    }
}