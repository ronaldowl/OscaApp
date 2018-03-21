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

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();  

    }
}


function setStatus() {

    var relacao = {
        id: "111" 
    } 

    
       
     
            $.ajax({
            url: 'http://localhost:49797/api/SetStatus',
                type: 'POST',
                data: JSON.stringify(relacao),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    WriteResponse(data);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });

       
} 