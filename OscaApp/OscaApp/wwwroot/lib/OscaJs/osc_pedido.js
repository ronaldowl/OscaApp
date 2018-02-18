 
function OnLoad_Pedido() {
    
    var statusForm = $("#osc_statusPedido").val(); 
    desabilita_Pedido(statusForm);
}

function desabilita_Pedido(status) {
    
    //Fechado ou cancelado
    if (status == 2 || status == 5) {

        //Desabilita todos campos do Form
        $("input,select, textarea, iframe").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);      
        $("#osc_calcular").hide(true);       
        $("#osc_encerrar").hide(true);    
    
                   
    } else {
        //Apresenta botoes
        $("#osc_reabrir").hide(false); 
    }
}