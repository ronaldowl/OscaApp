function OnLoad_ContaReceber() {

    Calendario("#osc_dataPagamento");
    
    var status = document.getElementById("osc_statusContaReceber").value; 
    var tipoLancamento = document.getElementById("osc_tipoLancamento").value; 

    ControleCamposAutomaticos(tipoLancamento)
    desabilitaCampos_ContaReceber(status)
}

function desabilitaCampos_ContaReceber(status) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();    
        $("#osc_novoPagamento").hide();                

    } else {

        //Desabilita bot�es
        //Esconde botoes
        $("#osc_reabrir").hide();  
    }
}

function ControleCamposAutomaticos(Lancamento) {

    if (Lancamento == 1  ) {

       //ProtegeCampo
        $("#osc_valor").prop("readonly", true);
        $("#osc_origemContaReceber").prop("readonly", true);
        $("#osc_titulo").prop("readonly", true);
        $("#osc_botaolookupcliente").prop("readonly", true);
        $("#osc_clienteIdName").prop("readonly", true);
        $("#osc_numeroReferencia").prop("readonly", true);



        


    }  
}

function OpenLookupCliente() {
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
    }
} 


function DeleteContasReceber(id) {

    DeleteService(id, "ContasReceberAPI", "GridContasReceber?view=0");
} 

function RecarregarGridContaReceber() {

    $(window.document.location).attr('href', "GridContasReceber?view=0");

}
function RecarregarGridContaReceberCliente(idCliente) {

    $(window.document.location).attr('href', "GridContasReceberCliente?view=0&idCliente=" + idCliente);

}

