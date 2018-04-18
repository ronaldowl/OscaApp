function OnLoad_Atendimento() {
    
    Calendario("#osc_dataInicio");
    OnChangeTipoReferencia();
    EscondeCamposInformacoes();
     
    var status = document.getElementById("osc_statusAtendimento").value; 
    desabilitaCampos_Atendimento(status)
}

function DeleteAtendimento(id) {  

    DeleteService(id, "AtendimentoAPI", "GridAtendimento?view=0");
} 

function RecarregarGridAtendimento() {

    $(window.document.location).attr('href', "GridAtendimento?view=0");

}

function OpenLookupCliente()
{
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
    }
}

function OpenLookupServico() {

    var idServico = $("#osc_IdServico").val();

    if (idServico != "") {
        $(window.document.location).attr('href', "/Servico/FormUpdateServico?id=" + idServico);
    }
}

 


function OpenLookupProfissional() {
    var idProffisional = $("#osc_IdProfissional").val();

    if (idProffisional != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Profissional/FormUpdateProfissional?id=" + idProffisional);
    }
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
