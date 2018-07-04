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
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Marco', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']
    });
}

function DeleteService(id, controller, redirect) {
    
    if (confirm("Deseja Excluir o Registro?")) {
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/API/" + controller + "/Delete",
            data: { id: id },
            success: function (dados) {
                if (dados.statusOperation == true) {
                    alert('Registro excluido com Sucesso!');
                    $(window.document.location).attr('href', redirect);
                }
            }
        });
    }
} 

function FormatMoney(valor) {

    valor = valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

    return valor;
}