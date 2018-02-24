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

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide(true);  

    }
}

function ConfigCalendario() {

    $(function () {
        $("#osc_dataInicio").datepicker({
            showButtonPanel: true,
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
        });
    }); 

}

