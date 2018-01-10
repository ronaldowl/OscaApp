using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework.Models
{
    public class Relacao
    {
        public Guid id { get; set; }
        public String idName { get; set; }
        public String organizacao { get; set; }
        public String codigo { get; set; }
        public Guid idOrganizacao { get; set; }
        public CustomEntityEnum.Entidade tipoObjeto { get; set; }
        public CustomEnumStatus.Status status { get; set; }


        public Relacao(Guid _id, CustomEntityEnum.Entidade entityType)
        {
            this.id = _id;
            this.tipoObjeto = entityType;
        }
        public Relacao(Guid _id, CustomEntityEnum.Entidade entityType, string _idName)
        {
            this.idName = _idName;
            this.id = _id;
            this.tipoObjeto = entityType;
        }
        public Relacao()
        {
        }

        //*********************** Métodos de Parse de Objetos do Osca

               
        public static List<Relacao> ConvertToRelacao(List<ListaPreco> itens)
        {          
            List<Relacao> lista = new List<Relacao>();         
                 
            foreach (var item in itens)
            {
                Relacao X = new Relacao();
                X.id = item.id;
                X.idName = item.nome;
                lista.Add(X);
            }
            return lista;
        }

        public static List<Relacao> ConvertToRelacao(List<Pedido> itens)
        {
            List<Relacao> lista = new List<Relacao>();
           
            foreach (var item in itens)
            {
                Relacao X = new Relacao();
                X.id = item.id;
                X.idName = item.codigoPedido;
                lista.Add(X);
            }
            return lista;
        }

        public static List<Relacao> ConvertToRelacao(List<ItemListaPreco> itens)
        {
            List<Relacao> lista = new List<Relacao>();
            
            foreach (var item in itens)
            {
                Relacao X = new Relacao();
                X.id = item.id;
               
                lista.Add(X);
            }
            return lista;
        }
    }
}
