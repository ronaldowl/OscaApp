function OnLoad_Atendimento() {
    
    ConfigCalendario();
     
    var status = document.getElementById("osc_statusAtendimento").value; 
    desabilitaCampos_Atendimento(status)
}

function desabilitaCampos_Atendimento(status) {

    if (status == 1 || status == 2 ) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);    

    } else {

        //Desabilita bot�es
        //Esconde botoes
        $("#osc_reabrir").hide(true);  

    }
}

function ConfigCalendario() {

    $(function () {
        $("#osc_dataInicio").datepicker({
            showButtonPanel: true,
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Ter�a', 'Quarta', 'Quinta', 'Sexta', 'S�bado', 'Domingo'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'S�b', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Mar�o', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
        });
    }); 

}

