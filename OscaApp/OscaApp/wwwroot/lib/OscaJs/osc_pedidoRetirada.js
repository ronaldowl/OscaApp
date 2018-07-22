
function OnLoad_PedidoRetirada() {

    //Configura calendario  
    ConfiguraDatas();

    var statusForm = $("#osc_statusPedidoRetirada").val();

    desabilita_PedidoRetirada(statusForm);

}

function DeletePedidoRetirada(id) {

    DeleteService(id, "PedidoRetiradaAPI", "GridPedidoRetirada?view=0");
} 

function RecarregarGridPedidoRetirada() {

    $(window.document.location).attr('href', "GridPedido?view=0");

}

function desabilita_PedidoRetirada(status) {

    //Fechado ou cancelado
    if (status == 2 || status == 3) {

        //Desabilita todos campos do Form
        $("input,select, textarea, iframe").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();
        $("#osc_botaoBuscaProfissional").hide(); 
        $("#osc_botaoBuscaCliente").hide(); 

        // $("#osc_calcular").hide();
        //$("#osc_novoProduto").hide();


    } else {
        //Apresenta botoes
        $("#osc_reabrir").hide();


    }
}

function ConfiguraDatas() {

    Calendario("#osc_dataEntrega");
    Calendario("#osc_dataRetirada");

}

 
 
