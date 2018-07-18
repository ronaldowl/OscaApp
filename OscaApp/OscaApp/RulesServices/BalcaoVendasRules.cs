using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class BalcaoVendasRules
    {
        public static Guid BalcaoVendasCreate(BalcaoVendasViewModel entrada,  ContextPage contexto, IBalcaoVendasData balcaoData)
        {
        
            entrada.balcaoVendas.codigo = AutoNumber.GeraCodigo(31, contexto.idOrganizacao);
            entrada.balcaoVendas.statusBalcaoVendas = CustomEnumStatus.StatusBalcaoVendas.Fechado;

            if(entrada.cliente != null) entrada.balcaoVendas.idCliente = entrada.cliente.id;

            if (entrada.balcaoVendas.codigo != null)
            {
                ////************ Objetos de controle de acesso ***************
                entrada.balcaoVendas.criadoEm = DateTime.Now;
                entrada.balcaoVendas.criadoPor = contexto.idUsuario;
                entrada.balcaoVendas.criadoPorName = contexto.nomeUsuario;
                entrada.balcaoVendas.modificadoEm = DateTime.Now;
                entrada.balcaoVendas.modificadoPor = contexto.idUsuario;
                entrada.balcaoVendas.modificadoPorName = contexto.nomeUsuario;
                entrada.balcaoVendas.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                //Grava objeto na base
                balcaoData.Add(entrada.balcaoVendas);              
               
            }
            return entrada.balcaoVendas.id;
        }
       
        public static bool GravaProdutoBalcao(ProdutoBalcao[] produtosBalcao, ContextPage contexto, SqlGeneric sqlGeneric, Guid idBalcao)
        {
            try
            {
                foreach (var item in produtosBalcao)
                {
                    sqlGeneric.InsereProdutoBalcao(MontaCreate( item, contexto, idBalcao));
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        public static bool FechaVenda(int valor, string idCliente, ClienteData clienteData, ContextPage contexto)
        {

            Cliente  modelo = new Cliente();
            modelo.id = new Guid(idCliente);
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;
            modelo.status = (CustomEnumStatus.Status)valor;
            clienteData.SetStatus(modelo);

            return true;            

        }

        public static  ProdutoBalcao MontaCreate(ProdutoBalcao produtoBalcao, ContextPage contexto,Guid idBalcao)
        {
            ProdutoBalcao retorno = new ProdutoBalcao();

            retorno = produtoBalcao;

            ////************ Objetos de controle de acesso ***************
            retorno.idBalcaoVenda = idBalcao;
            retorno.idOrganizacao = contexto.idOrganizacao;
            ////************ FIM Objetos de controle de acesso ***************
            
            return retorno;
        }

 
    }
}
