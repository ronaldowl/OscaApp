function RecarregarGrid() {

    document.getElementById("grid").value = ""; 
}

function desabilitaCampos(status) {

    if (status == "Inativo") {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide(true);
        $("#osc_status_inativa").hide(true);
        
    }
}


function Calendario(campo) {     

    $(campo).datepicker({
        showButtonPanel: true,
        defaultViewDate: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Ter�a', 'Quarta', 'Quinta', 'Sexta', 'S�bado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'S�b', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Marco', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
    });
}
