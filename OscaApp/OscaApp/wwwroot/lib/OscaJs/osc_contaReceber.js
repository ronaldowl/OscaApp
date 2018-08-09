function OnLoad_ContaReceber() {

    Calendario("#osc_dataPagamento");
    
    var status = document.getElementById("osc_statusContaReceber").value; 
    desabilitaCampos_ContaReceber(status)
}

function desabilitaCampos_ContaReceber(status) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();    

    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();  

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

