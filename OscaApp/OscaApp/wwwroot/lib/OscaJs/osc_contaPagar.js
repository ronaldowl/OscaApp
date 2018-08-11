function OnLoad_ContaPagar() {

    Calendario("#osc_dataPagamento");
    Calendario("#osc_dataFechamento");

     
    var statusContaPagar = document.getElementById("osc_statusContaPagar").value; 
    desabilitaCampos_ContaPagar(statusContaPagar);
}

function desabilitaCampos_ContaPagar(statusContaPagar) {

    if (statusContaPagar == 1) {

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

function DeleteContasPagar(id) {

    DeleteService(id, "ContasPagarAPI", "GridContasPagar?view=0");
} 

function RecarregarGridContaPagar() {

    $(window.document.location).attr('href', "GridContasPagar?view=0");

}