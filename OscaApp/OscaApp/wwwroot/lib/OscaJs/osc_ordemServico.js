
function OnLoad_OrdemServico() {
    
    
    var statusForm = $("#osc_statusOrdemServico").val(); 

    desabilita_OrdemServico(statusForm);
  
}

function DeleteOrdemServico(id) {

    DeleteService(id, "OrdemServicoAPI", "GridOrdemServico?view=0");
} 

function RecarregarGridOrdemServico() {

    $(window.document.location).attr('href', "GridOrdemServico?view=0");

}

function OpenLookupCliente() {
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
    }
}  

function OpenLookupProfissional() {
    var id = $("#osc_IdProfissional").val();

    if (id != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Profissional/FormUpdateProfissional?id=" + id);
    }
}  


function desabilita_OrdemServico(status) {

    //Fechado ou cancelado
    if (status == 2 || status ==5) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();      
        $("#osc_calcular").hide();
        $("#osc_novoServicoOrdem").hide(); 
        $("#osc_novoProdutoOrdem").hide();      

                        
        $("#osc_botaoBuscaProfissional").hide(); 
        $("#botaoLookupCliente").hide();  
        $("#botaoLookupCategoriaManutencao").hide();                    

    } else {
        //Esconde botões
        $("#osc_reabrir").hide(); 

        //Executa regra para tratamento dos campos de desconto
        Onchage_OrdemServicoTipoDesconto();
    }

}

function Onchage_OrdemServicoTipoDesconto() {    

    var tipo = document.getElementById("osc_tipoDesconto");
    var descontoValor = document.getElementById("osc_valorDesconto");
    var descontoPercentual = document.getElementById("osc_valorDescontoPercentual");

    if (tipo.value == 1) {

        $("#osc_valorDescontoPercentual").prop('disabled', true);
        $("#osc_valorDesconto").prop('disabled', false);
        descontoPercentual.value = "0";

    }

    if (tipo.value == 2) {

        $("#osc_valorDescontoPercentual").prop('disabled', false);
        $("#osc_valorDesconto").prop('disabled', true);
        descontoValor.value = "0";

    }
}