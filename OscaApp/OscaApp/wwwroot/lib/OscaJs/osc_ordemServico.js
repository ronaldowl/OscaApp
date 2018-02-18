
function OnLoad_OrdemServico() {
    debugger;
    
    var statusForm = $("#osc_statusOrdemServico").val(); 
    desabilita_OrdemServico(statusForm);
}

function desabilita_OrdemServico(status) {

    //Fechado ou cancelado
    if (status == 2 || status ==5) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);      
        $("#osc_calcular").hide(true);
        $("#osc_novoServico").hide(true);
        $("#osc_encerrar").hide(true);  
        $("#botaoLookupCliente").hide(true);  
        $("#botaoLookupCategoriaManutencao").hide(true);                    

    } else {
        //Apresenta botoes
        $("#osc_reabrir").hide(false); 
    }

}