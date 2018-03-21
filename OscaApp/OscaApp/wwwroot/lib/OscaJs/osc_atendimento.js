function OnLoad_Atendimento() {
  
    
    Calendario("#osc_dataInicio");
    OnChangeTipoReferencia();
     
    var status = document.getElementById("osc_statusAtendimento").value; 
    desabilitaCampos_Atendimento(status)
}

function desabilitaCampos_Atendimento(status) {
  
    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        $("#osc_buscaCliente").hide();
        $("#osc_botaoBuscaProfissional").hide();
        $("#osc_botaoBuscaOS").hide();
        $("#osc_botaoBuscaServico").hide();


        //Esconde botoes
        $("#osc_salvar").hide();    

    } else {

        //Desabilita bot�es
        //Esconde botoes
        $("#osc_reabrir").hide();  

    }
}


function ValidaHora(hora)
{
    
    var inicio = $("#osc_horaInicio").val();
    var fim = $("#osc_horaFim").val();

    if (fim <= inicio)
    { 

        alert("O hor�rio do fim n�o pode ser maior ou igual ao horario do inicio!");

        if (hora == 1) {
            $("#osc_horaInicio").val("15");
        }
        if (hora == 2) {
            $("#osc_horaFim").val("17");
        }

    }

}

function OnChangeTipoReferencia()
{
    var tipo = $("#osc_tipoReferencia").val();   

    if (tipo == 0) {      

        $("#osc_IdNameOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_botaoBuscaOS").hide();

        $("#osc_IdNameServico").val("");
        $("#osc_IdNameServico").prop("disabled", true);
        $("#osc_botaoBuscaServico").hide();
        $("#osc_valor").val("0,00");
        $("#osc_valor").prop("disabled", true);

    }

    
    if (tipo == 1)
    {
        $("#osc_IdNameOs").val("");
        $("#osc_IdOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_botaoBuscaOS").hide();

        //habilita
        $("#osc_IdNameServico").prop("disabled", false);
        $("#osc_botaoBuscaServico").show();
        $("#osc_valor").prop("disabled", false);

    }

    if (tipo == 2)
    {              
        $("#osc_IdNameServico").val("");
        $("#osc_IdServico").val("");
        $("#osc_IdNameServico").prop("disabled", true);
        $("#osc_botaoBuscaServico").hide();

        //habilita
        $("#osc_IdNameOs").prop("disabled", false);
        $("#osc_botaoBuscaOS").show();
        $("#osc_valor").prop("disabled", false);
    }
}
