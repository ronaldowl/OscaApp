//********************************* Biblioteca com funções da Tela de Pedido ***************

function OnLoad() {
    //********* Executa regra do Onload do Form **********************
    debugger;

    // PARA HABILITAR OS CAMPOS DO FORMULÁRIO, MUDAR O true PARA false
    var statusForm = $("#osc_statusPedido").val();

    //Desabilita campos se o registro estiver INATIVO
    desabilita_Pedido(statusForm);
}


function desabilita_Pedido(status) {

    if (status == "Inativo") {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);
        $("#osc_status_inativa").hide(true);
        $("#osc_endereco").hide(true);

    } else {

        $("#osc_status_Ativar").hide(true);

    }

}