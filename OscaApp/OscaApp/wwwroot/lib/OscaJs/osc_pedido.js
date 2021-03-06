
function OnLoad_Pedido() {

    var statusForm = $("#osc_statusPedido").val();

    desabilita_Pedido(statusForm);

}

function DeletePedido(id) {

    DeleteService(id, "PedidoAPI", "GridPedido?view=0");
} 

function RecarregarGridPedido() {

    $(window.document.location).attr('href', "GridPedido?view=0");

}

function desabilita_Pedido(status) {

    //Fechado ou cancelado
    if (status == 2 || status == 5) {

        //Desabilita todos campos do Form
        $("input,select, textarea, iframe").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();
        $("#osc_calcular").hide();
        $("#osc_novoProduto").hide();


    } else {
        //Apresenta botoes
        $("#osc_reabrir").hide();

        //Regra para tratamento dos campos de desconto
        Onchage_PedidoTipoDesconto();
    }
}

function Onchage_PedidoTipoDesconto() {

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

function OpenLookupCliente() {

    var id = $("#osc_clienteId").val();

    if (id != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + id);
    }
}

 
