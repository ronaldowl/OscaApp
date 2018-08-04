
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

function CalculaLinha1() {
    var QTD = $("#osc_quantidade1").val();
    var VALOR = $("#osc_valor1").val();
    var TOTAL = "";

    VALOR = parseFloat(PrepCalcDecimal(VALOR));

    TOTAL = QTD * VALOR;

    $("#osc_valorTotal1").val(FormataDecimal(TOTAL));

    CalculaValorTotal();
}

function CalculaLinha2() {
    var QTD = $("#osc_quantidade2").val();
    var VALOR = $("#osc_valor2").val();
    var TOTAL = "";

    VALOR = parseFloat(PrepCalcDecimal(VALOR));

    TOTAL = QTD * VALOR;

    $("#osc_valorTotal2").val(FormataDecimal(TOTAL));

    CalculaValorTotal();
}

function CalculaValorTotal() {

    var VALOR1 = $("#osc_valorTotal1").val();
    var VALOR2 = $("#osc_valorTotal2").val();

    var QTD1 = $("#osc_quantidade1").val();
    var QTD2 = $("#osc_quantidade2").val();
    var TOTAL = 0;
    var TOTALQTD

    VALOR1 = parseFloat(PrepCalcDecimal(VALOR1));
    VALOR2 = parseFloat(PrepCalcDecimal(VALOR2));

    QTD1 = parseFloat(PrepCalcDecimal(QTD1));
    QTD2 = parseFloat(PrepCalcDecimal(QTD2));
    TOTAL = VALOR1 + VALOR2;
    TOTALQTD = QTD1 + QTD2;  

    $("#osc_valorTotal").val(FormataDecimal(TOTAL));
    $("#osc_quantidade").val(TOTALQTD);
        
}

function LimpaEndereco1()
{
    $("#osc_enderecoId").val("");
    $("#osc_enderecoIdName").val("");

}

function LimpaEndereco2() {
    $("#osc_enderecoId2").val("");
    $("#osc_enderecoIdName2").val("");

}