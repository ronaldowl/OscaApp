
function OnLoad_OrdemServico() {
    debugger;
    
    var statusForm = $("#osc_statusOrdemServico").val(); 

    desabilita_OrdemServico(statusForm);
  
}

function desabilita_OrdemServico(status) {

    //Fechado ou cancelado
    if (status == 2 || status ==5) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);      
        $("#osc_calcular").hide(true);
        $("#osc_novoServico").hide(true);
        
        $("#botaoLookupCliente").hide(true);  
        $("#botaoLookupCategoriaManutencao").hide(true);                    

    } else {
        //Apresenta botoes
        $("#osc_reabrir").hide(false); 

        //Executa regra para tratamento dos campos de desconto
        Onchage_OrdemServicoTipoDesconto();
    }

}

function Onchage_OrdemServicoTipoDesconto() {
    debugger;

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