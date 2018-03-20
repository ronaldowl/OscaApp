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
