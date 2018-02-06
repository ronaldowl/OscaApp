//********************************* Biblioteca com funções da Tela de Pedido ***************

function OnLoad() {
    //********* Executa regra do Onload do Form **********************
    debugger;

    // PARA HABILITAR OS CAMPOS DO FORMULÁRIO, MUDAR O true PARA false
    var statusForm = $("#osc_status").val();

    //Desabilita campos se o registro estiver INATIVO
    desabilita_osc_MainFormUpdate(statusForm);
}
