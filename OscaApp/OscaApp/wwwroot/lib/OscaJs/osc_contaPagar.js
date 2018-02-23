function OnLoad_ContaPagar() {
    debugger;
     
    var status = document.getElementById("osc_statusContaPagar").value; 
    desabilitaCampos_ContaPagar(status)
}

function desabilitaCampos_ContaPagar(status) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);        
        
    }
}
