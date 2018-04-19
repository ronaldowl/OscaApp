function OnLoad_Agendamento() {
    
    Calendario("#osc_dataInicio");
    OnChangeTipoReferencia();    
     
    var status = document.getElementById("osc_statusAgendamento").value; 
    desabilitaCampos_Agendamento(status)
}

function DeleteAgendamento(id) {  

    DeleteService(id, "AgendamentoAPI", "GridAgendamento?view=0");
} 

function RecarregarGridAgendamento() {

    $(window.document.location).attr('href', "GridAgendamento?view=0");

}

function OpenLookupCliente()
{
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
    }
}

function OpenLookupPedido() {

    var idPedido = $("#osc_IdPedido").val();

    if (idServico != "") {
        $(window.document.location).attr('href', "/Pedido/FormUpdatePedido?id=" + idPedido);
    }
}

function OpenLookupAtendimento() {

    var idAtendimento = $("#osc_IdAtendimento").val();

    if (idAtendimento != "") {
        $(window.document.location).attr('href', "/Atendimento/FormUpdateAtendimento?id=" + idAtendimento);
    }
}

function OpenLookupOS() {

    var idOS = $("#osc_IdOs").val();

    if (idOS != "") {
        $(window.document.location).attr('href', "/OrdemServico/FormUpdateOrdemServico?id=" + idOS);
    }
}


function OpenLookupProfissional() {
    var idProffisional = $("#osc_IdProfissional").val();

    if (idProffisional != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Profissional/FormUpdateProfissional?id=" + idProffisional);
    }
}



function OnChangeTipoReferencia( ) {
     
  
    var tipo = $("#osc_tipoReferencia").val();

    if (tipo == 0) {

        $("#osc_imprimir").show();

        //Habilita Atendimento
 
        $("#osc_IdNameAtendimento").prop("disabled", false);
        $("#osc_IdNameAtendimento").attr("required", true);
        $("#osc_botaoBuscaAtendimento").show();  

        //Desabilita OS
        $("#osc_IdNameOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_IdNameOs").attr("required", false);
        $("#osc_botaoBuscaOS").hide();

        //Disabilita Pedido
        $("#osc_IdNamePedido").val("");
        $("#osc_IdNamePedido").prop("disabled", true);
        $("#osc_IdNamePedido").attr("required", false);
        $("#osc_botaoBuscaPedido").hide();


    }


    if (tipo == 2) {
        $("#osc_imprimir").show();

        //Habilita OS
    
        $("#osc_IdNameOs").prop("disabled", false);
        $("#osc_IdNameOs").attr("required", true);
        $("#osc_botaoBuscaOS").show();


        //Disabilita Pedido
        $("#osc_IdNamePedido").val("");
        $("#osc_IdNamePedido").prop("disabled", true);
        $("#osc_IdNamePedido").attr("required", false);
        $("#osc_botaoBuscaPedido").hide();

        //Disabilita Atendimento
        $("#osc_IdNameAtendimento").val("");
        $("#osc_IdNameAtendimento").prop("disabled", true);
        $("#osc_IdNameAtendimento").attr("required", false);
        $("#osc_botaoBuscaAtendimento").hide();        

    }

    if (tipo == 3) {
        $("#osc_imprimir").hide();

        //Habilita Pedido
    
        $("#osc_IdNamePedido").prop("disabled", false);
        $("#osc_IdNamePedido").attr("required", true);
        $("#osc_botaoBuscaPedido").show();


        //Desabilita OS
        $("#osc_IdNameOs").val("");
        $("#osc_IdNameOs").prop("disabled", true);
        $("#osc_IdNameOs").attr("required", false);
        $("#osc_botaoBuscaOS").hide();

        //Desabilita Atendimento
        $("#osc_IdNameAtendimento").val("");
        $("#osc_IdNameAtendimento").prop("disabled", true);
        $("#osc_IdNameAtendimento").attr("required", false);
        $("#osc_botaoBuscaAtendimento").hide();  

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

function desabilitaCampos_Agendamento(status) {

    if (status == 1 || status == 2) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        $("#osc_buscaCliente").hide();
        $("#osc_botaoBuscaProfissional").hide();
        $("#osc_botaoBuscaOS").hide();
        $("#osc_botaoBuscaAtendimento").hide();
        $("#osc_botaoBuscaPedido").hide();



        //Esconde botoes
        $("#osc_salvar").hide();

    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();

    }
}