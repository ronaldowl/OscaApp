function RecarregarGrid() {

    document.getElementById("grid").value = ""; 
}

function desabilitaCampos(status) {

    if (status == "Inativo") {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);
        $("#osc_status_inativa").hide(true);
        
    }
}
