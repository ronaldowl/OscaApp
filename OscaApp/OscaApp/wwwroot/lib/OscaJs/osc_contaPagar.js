function OnLoad_ContaPagar() {

    Calendario("#osc_dataPagamento");
     
    var statusContaPagar = document.getElementById("osc_statusContaPagar").value; 
    desabilitaCampos_ContaPagar(status)
}

function desabilitaCampos_ContaPagar(statusContaPagar) {

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


function desabilitaCampos_Status(status)
{

    

    if (status == 1 || status == 2) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);

    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide(true);

    }
}

function DeleteContasPagar(id) {

    DeleteService(id, "ContasPagarAPI", "GridContasPagar?view=0");
} 
