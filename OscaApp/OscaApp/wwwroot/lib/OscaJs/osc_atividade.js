function OnLoad_Atividade() {    
     
    Calendario("#osc_dataAlvo");
     
    var status = document.getElementById("osc_statusAtividade").value; 
    desabilitaCampos_Atividade(status)
}

function desabilitaCampos_Atividade(status) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();    
        $("#osc_botaoBuscaProfissional").hide();    

    } else {

        //Desabilita bot�es
        //Esconde botoes
        $("#osc_reabrir").hide();  

    }
}

function DeleteAtividade(id) {
    

    DeleteService(id, "AtividadeAPI", "GridAtividade?view=0");      
} 

function RecarregarGridAtividade() {

    $(window.document.location).attr('href', "GridAtividade?view=0");

}