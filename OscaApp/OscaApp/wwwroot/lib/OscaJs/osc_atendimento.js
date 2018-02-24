function OnLoad_Atendimento() {
    debugger;
     
    var status = document.getElementById("osc_statusAtendimento").value; 
    desabilitaCampos_Atendimento(status)
}

function desabilitaCampos_Atendimento(status) {

    if (status == 1 || status == 2 ) {

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
