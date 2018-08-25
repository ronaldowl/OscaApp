 using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.MicroServices;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OscaApp.framework
{
    public class CargaInicial
    {

        public void CreateProfissional(ApplicationUser usuario, ContextPage contexto, ContexDataService dbContext)
        {
            Profissional profissional = new Profissional();
            SqlGeneric sqlservice = new SqlGeneric();

            profissional.codigo = sqlservice.RetornaNovaPosicao(17, contexto.idOrganizacao);
            profissional.nomeProfissional = usuario.nomeAmigavel;
            profissional.idUsuario = new Guid(usuario.Id);

            //************ Objetos de controle de acesso ******************
            profissional.criadoEm = DateTime.Now;
            profissional.criadoPor = contexto.idUsuario;
            profissional.criadoPorName = contexto.nomeUsuario;
            profissional.modificadoEm = DateTime.Now;
            profissional.modificadoPor = contexto.idUsuario;
            profissional.modificadoPorName = contexto.nomeUsuario;
            profissional.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ***************

            ProfissionalData profissionalData = new ProfissionalData(dbContext);

            profissionalData.Add(profissional);

        }

        public void CreatePerfis(ApplicationUser usuario, ContextPage contexto, ContexDataService dbContext)
        {
            //******************************************  ADMINISTRADOR *****************************
            PerfilAcesso Administrador = new PerfilAcesso();
            Administrador.nome = "Administrador";
            Administrador.status = CustomEnumStatus.Status.Ativo;
            //************ Objetos de controle de acesso ******************
            Administrador.criadoEm = DateTime.Now;
            Administrador.criadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Administrador.criadoPorName = "OscaAdmin";
            Administrador.modificadoEm = DateTime.Now;
            Administrador.modificadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Administrador.modificadoPorName  = "OscaAdmin";
            Administrador.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ***************
            //************ Acesso  **********************
            Administrador.permitePainelGerenciamento    = true;
            Administrador.permitePainelCadastro         = true;
            Administrador.permitePainelConfiguracoes    = true;
            Administrador.permitePainelFinanceiro       = true;
            Administrador.permitePainelGlobal           = true;
            Administrador.permitePainelHome             = true;
            Administrador.permitePainelOperacional      = true;
            Administrador.permitePainelServico          = true;
            Administrador.permitePainelSuporte          = true;
            Administrador.permitePainelVendas           = true;
            //************ Fim Acesso **********************
            //************ Acesso Registros ****************

            //************ Fim Acesso Registros ****************
            //**************************************************  FIM ADMINISTRADOR *****************************

            //**************************************************  COMPLETO *****************************
            PerfilAcesso Completo = new PerfilAcesso();
            Completo.nome = "Completo";
            Completo.status = CustomEnumStatus.Status.Ativo;
            //************ Objetos de controle de acesso ******************
            Completo.criadoEm = DateTime.Now;
            Completo.criadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Completo.criadoPorName = "OscaAdmin";
            Completo.modificadoEm = DateTime.Now;
            Completo.modificadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Completo.modificadoPorName = "OscaAdmin";
            Completo.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ************

            //************ Acesso Completo **********************
            Completo.permitePainelGerenciamento     = false;
            Completo.permitePainelCadastro          = true;
            Completo.permitePainelConfiguracoes     = true;
            Completo.permitePainelFinanceiro        = true;
            Completo.permitePainelGlobal            = true;
            Completo.permitePainelHome              = true;
            Completo.permitePainelOperacional       = true;
            Completo.permitePainelServico           = true;
            Completo.permitePainelSuporte           = true;
            Completo.permitePainelVendas            = true;
            //************ Fim Acesso **********************
            //************ Acesso Registros ****************

            //************ Fim Acesso Registros ****************
            //******************************************************** FIM COMPLETO *********************************************
            
            PerfilAcesso Basico = new PerfilAcesso();
            Basico.nome = "Basico";
            Basico.status = CustomEnumStatus.Status.Ativo;
            //************ Objetos de controle de acesso ******************
            Basico.criadoEm = DateTime.Now;
            Basico.criadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Basico.criadoPorName = "OscaAdmin";
            Basico.modificadoEm = DateTime.Now;
            Basico.modificadoPor = new Guid("6E835F74-5249-4B36-AF2E-21F89D1E5964");
            Basico.modificadoPorName = "OscaAdmin";
            Basico.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ***************
            //*************************************************   BASICO ************************************************************
            Basico.permitePainelGerenciamento     = false;
            Basico.permitePainelCadastro          = true;
            Basico.permitePainelConfiguracoes     = false;
            Basico.permitePainelFinanceiro        = true;
            Basico.permitePainelGlobal            = true;
            Basico.permitePainelHome              = true;
            Basico.permitePainelOperacional       = true;
            Basico.permitePainelServico           = true;
            Basico.permitePainelSuporte           = true;
            Basico.permitePainelVendas            = true;
            //************ Fim Acesso **********************
            //************ Acesso Registros ****************

            //************ Fim Acesso Registros ****************
            //************************************************* FIM BASICO ************************************************************
            // Executa a Criação do Perifl 
            PerfilAcessoData perfilData = new PerfilAcessoData(dbContext);

            perfilData.Add(Administrador);
            perfilData.Add(Completo);
            perfilData.Add(Basico);

        }

        public void CreateOrgConfig(  ContextPage contexto, ContexDataService dbContext)
        {
            OrgConfig modelo = new OrgConfig();

            //sessão Produto
            modelo.quantidadeMinimaProduto = 5;
            modelo.margemBaseProduto = 25;
            //sessão contas Receber
            modelo.qtdDiasCartaoCredito = 28;
            modelo.qtdDiasCartaoDebito = 1;
            //sessão Cupom
            modelo.cupom_altura = "90%";
            modelo.cupom_largura = "450px";
            modelo.cupom_fontesize = "12px";
            modelo.cupom_altura = "90%";
            modelo.mensagemCupom = "Defina sua mensagem personalizada";

            //sessão Pedido Retirada
            modelo.mensagemPedido = " Defina a sua mensagem personalidada no caminhjo  Configurações - Parametros - Configurações do Sistema";
            modelo.tituloImpressao = "Defina o seu titulo";


            //************ Objetos de controle de acesso ******************
            modelo.criadoEm = DateTime.Now;
            modelo.criadoPor = contexto.idUsuario;
            modelo.criadoPorName = contexto.nomeUsuario;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;
            modelo.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ***************
    
            OrgConfigData orgConfigData = new OrgConfigData(dbContext);

            orgConfigData.Add(modelo);

        }

    }
}
