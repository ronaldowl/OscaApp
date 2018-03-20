function OnLoad_ContaPagar() {

    Calendario("#osc_dataPagamento");
     
    var statusContaPagar = document.getElementById("osc_statusContaPagar").value; 
    desabilitaCampos_ContaPagar(status)
}

function desabilitaCampos_ContaPagar(statusContaPagar) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();    

    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();  

    }
}


function desabilitaCampos_Status(status)
{

    

    if (status == 1 || status == 2) {

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