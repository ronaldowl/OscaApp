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
function FormatMoneyDecimal(valor) {

    valor = Number(valor).toFixed(2);

    return valor;
}




function ValidaCPF() {

    var val = document.getElementById("osc_cpf");
    var tipo = "CPF";


    var bolValida = true;
    var chkValidado = true;

    if (tipo == "CPF") {
        chkValidado = chkCpf(unFormatNumber(val.value));
    }
    if (chkValidado == false) {
        val.value = ""
    }
    else {

        if (tipo == "CPF") {
            $('#osc_cpf').val( formatCPF(unFormatNumber(val.value)));
        }
    }
}


function chkCpf(valor) {
    valor = unFormatNumber(valor);
    if (unFormatNumber(valor) == "11111111111" || unFormatNumber(valor) == "22222222222" || unFormatNumber(valor) == "33333333333" || unFormatNumber(valor) == "44444444444" || unFormatNumber(valor) == "55555555555" || unFormatNumber(valor) == "66666666666" || unFormatNumber(valor) == "77777777777" || unFormatNumber(valor) == "88888888888" || unFormatNumber(valor) == "99999999999" || unFormatNumber(valor) == "00000000000") {
        alert('CPF Inválido.');
        return false;
    }
    var strcpf = valor;
    var str_aux = "";
    for (i = 0; i <= strcpf.length - 1; i++) {
        if ((strcpf.charAt(i)).match(/\d/)) str_aux += strcpf.charAt(i);
        else if (!(strcpf.charAt(i)).match(/[\.\-]/)) {
            alert('CPF inválido. Caracteres inválidos.');
            return false;
        }
    }
    if (str_aux.length != 11) {
        alert('CPF inválido. Quantidade de dígitos inválida.');
        return false;
    }
    soma1 = soma2 = 0;
    for (i = 0; i <= 8; i++) {
        soma1 += str_aux.charAt(i) * (10 - i);
        soma2 += str_aux.charAt(i) * (11 - i);
    }
    d1 = ((soma1 * 10) % 11) % 10;
    d2 = (((soma2 + (d1 * 2)) * 10) % 11) % 10;
    if ((d1 != str_aux.charAt(9)) || (d2 != str_aux.charAt(10))) {
        alert('CPF Inválido.');
        return false;
    }
    return true;
}

function unFormatNumber(valor) {
    if (valor != null) {
        var desformated = "";
        while (valor.indexOf(".") != -1) {
            valor = valor.replace('.', '');
        }
        while (valor.indexOf("-") != -1) {
            valor = valor.replace('-', '');
        }
        while (valor.indexOf("/") != -1) {
            valor = valor.replace('/', '');
        }
        while (valor.indexOf("\\") != -1) {
            valor = valor.replace('\\', '');
        }
        return valor;
    }
}

function formatCPF(valor) {
    var cpf_Pt1 = valor.substring(0, 3);
    var cpf_Pt2 = valor.substring(3, 6);
    var cpf_Pt3 = valor.substring(6, 9);
    var cpf_Pt4 = valor.substring(9, 11);
    var ponto = ".";
    var traco = "-";
    return cpf_Pt1 + ponto + cpf_Pt2 + ponto + cpf_Pt3 + traco + cpf_Pt4;
}
