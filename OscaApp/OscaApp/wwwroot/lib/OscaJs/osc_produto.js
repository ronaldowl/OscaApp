function OnLoad_Produto() {
 
    var status = document.getElementById("osc_status").value; 
    desabilitaCampos_Produto(status);
}

function desabilitaCampos_Produto(status) {

    if (status == 0) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_inativar").hide();  
        $("#osc_salvar").hide();    


    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();  
        $("#osc_ativar").hide();  

       


    }
}

function SetStatusProduto(id, valor) {

    if (valor == 0) {

        if (confirm("Deseja Inativar o Registro? não será possivel mais usar esse Produto!")) {

            SetStatus(id, valor, 'Produto');
        }
    }

    if (valor == 1) {

        if (confirm("Deseja Ativar o Registro?")) {

            SetStatus(id, valor, 'Produto');
        }
    }

}


 