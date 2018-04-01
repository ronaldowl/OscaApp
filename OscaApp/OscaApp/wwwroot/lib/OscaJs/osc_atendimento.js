function OnLoad_Atendimento() {
    
    Calendario("#osc_dataInicio");
    OnChangeTipoReferencia();
    EscondeCamposInformacoes();
     
    var status = document.getElementById("osc_statusAtendimento").value; 
    desabilitaCampos_Atendimento(status)
}

function OpenLookupCliente(id)
{
    $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + id); 
}

function OpenLookupServico(id) {
    $(window.document.location).attr('href', "/Servico/FormUpdateServico?id=" + id);
}

function OpenLookupProfissional(id) {
    $(window.document.location).attr('href', "/Profissional/FormUpdateProfissional?id=" + id);
}


function EscondeCamposInformacoes() {
    var tipo = document.getElementById("osc_tipoReferencia").value;

    if ( tipo == 2)
    {
        $("#informacoes").hide();
    } else {
        $("#informacoes").show();

    }     
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

        //Desabilita botões
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

        alert("O horário do fim não pode ser maior ou igual ao horario do inicio!");

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
    EscondeCamposInformacoes();
    var tipo = $("#osc_tipoReferencia").val();   

    if (tipo == 0) {      

        $("#osc_IdNameOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_IdNameOs").attr("required", false);   

        $("#osc_botaoBuscaOS").hide();

        $("#osc_IdNameServico").val("");
        $("#osc_IdNameServico").prop("disabled", true);
        $("#osc_IdNameServico").attr("required", false);  

        $("#osc_botaoBuscaServico").hide();
        //$("#osc_valor").val("0,00");
        //$("#osc_valor").prop("disabled", true);
        $("#osc_imprimir").show();        

    }

    
    if (tipo == 1)
    {
        $("#osc_imprimir").show();

        $("#osc_IdNameOs").val("");
        $("#osc_IdOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_botaoBuscaOS").hide();
        $("#osc_IdNameOs").attr("required", false);   

        $("#osc_IdNameServico").attr("required", true);    
        
        
        //habilita
        $("#osc_IdNameServico").prop("disabled", false);
        $("#osc_botaoBuscaServico").show();
        $("#osc_valor").prop("disabled", false);

    }

    if (tipo == 2)
    {              
        $("#osc_imprimir").hide();  

        $("#osc_IdNameServico").val("");
        $("#osc_IdServico").val("");
        $("#osc_IdNameServico").prop("disabled", true);
        $("#osc_botaoBuscaServico").hide();
        $("#osc_IdNameServico").attr("required", false);   

        //habilita
        $("#osc_IdNameOs").prop("disabled", false);
        $("#osc_IdNameOs").attr("required", true);   

        $("#osc_botaoBuscaOS").show();
        $("#osc_valor").prop("disabled", false);
    }
}
