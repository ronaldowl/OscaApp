//********************************* Biblioteca com funções da Tela de Cliente ***************

function OnLoad() {

    var status = $("#osc_status").val();
    desabilita_Cliente(status);
  
}
 
function OnChangeFiltro() {

    var filtro = $("#osc_filtro").val();

    if (filtro == "0") { $(window.document.location).attr('href', "GridClientePotencial?view=0"); }
    if (filtro == "1") { $(window.document.location).attr('href', "GridClientePotencial?view=1"); }


}

 

function RecarregarGridConta() {

    $(window.document.location).attr('href', "GridClientePotencial?view=1");

}

function SetStatusCliente(id, valor) {

    if (valor == 0) {

        if (confirm("Deseja Inativar o Registro? não será possivel mais usar esse cliente!")) {

            SetStatus(id, valor);
        }
    }

    if (valor == 1) {

        if (confirm("Deseja Ativar o Registro?")) {

            SetStatus(id, valor);
        }
    }

}

function SetStatus(id, valor ) {

    
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/API/ClienteAPI/SetStatus",
            data: {
                valor: valor,
                idCliente: id
            },
            success: function (dados) {
                if (dados.statusOperation == true) {
                    alert('Registro atualizado com sucesso!');
                    $(window.document.location).attr('href', 'FormUpdateCliente?id=' + id);
                } else {
                    alert('Falha ao atualizar! - ' + dados.statusMensagem);
                }
            }
        });
    
} 

function OnChangeTipoPessoa()
{   
    LimpaCNPJ_CPF();
}

 
 
function desabilita_Cliente(status) {

    if (status == "Inativo") {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();
        $("#osc_status_inativa").hide();
        $("#osc_endereco").hide();
        $("#osc_ordemservico").hide();
        $("#osc_atendimento").hide();
        $("#osc_pedido").hide();
        $("#osc_conta").hide();  
        $("#botaoBuscarCliente").hide(); 
                

        $("#osc_inativar").hide();      
        $("#osc_ativar").show();
        

    } else {
        
        $("#osc_ativar").hide();
        $("#osc_inativar").show();

    }

}

function ValidaCNPJ_CPF()
{  
    var val = document.getElementById("osc_cnpj_cpf");
    var tipo = "";

    if (document.getElementById("osc_tipopessoa").value == 1) {
        tipo = "CPF";
    }
    else {
        tipo = "CNPJ";
    }

    var bolValida = true;
    var chkValidado = true;
    if (tipo == "CPF") {
        chkValidado = chkCpf(unFormatNumber(val.value));
    }
    else {
        chkValidado = chkCnpj(unFormatNumber(val.value));
    }
    if (chkValidado == false) {
        val.value = "";
      
    }
    else {

        if (tipo == "CPF") {
            val.value = formatCPF(unFormatNumber(val.value));
        }
        else {
            val.value = formatCNPJ(unFormatNumber(val.value));
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
        alert('CPF inválido. Quatidade de dígitos inválida.');
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

function chkCnpj(valor) {
    if (valor.length < 14) {
        alert('CNPJ Inválido. Número de caracteres inválido!');
        return false;
    }
    try {
        valor = unFormatNumber(valor);
        var i;
        valor = valor.replace(".", "").replace("-", "").replace("/", "").replace(".", "");
        var c = valor.substr(0, 12);
        var dv = valor.substr(12, 2);
        var d1 = 0;
        for (i = 0; i < 12; i++) {
            d1 += c.charAt(11 - i) * (2 + (i % 8));
        }
        if (d1 == 0) {
            alert('CNPJ Inválido!!!. Número de caracteres inválido!');
            return false;
        }
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(0) != d1) {
            alert('CNPJ Inválido!');
            return false;
        }
        d1 *= 2;
        for (i = 0; i < 12; i++) {
            d1 += c.charAt(11 - i) * (2 + ((i + 1) % 8));
        }
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(1) != d1) {
            alert('CNPJ Inválido.');
            return false;
        }
        return true;
    }
    catch (e) {
        alert('CNPJ Inválido.');
        return false;
    }
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

function formatCNPJ(valor) {
    //debugger;
    var cnpj_Pt1 = valor.substring(0, 2);
    var cnpj_Pt2 = valor.substring(2, 5);
    var cnpj_Pt3 = valor.substring(5, 8);
    var cnpj_Pt4 = valor.substring(8, 12);
    var cnpj_Pt5 = valor.substring(12, 14);
    var ponto = ".";
    var traco = "-";
    var barra = "/";
    return cnpj_Pt1 + ponto + cnpj_Pt2 + ponto + cnpj_Pt3 + barra + cnpj_Pt4 + traco + cnpj_Pt5;
}

function UpcaseForGuid(G) {

    var GG = G.split()
    var GUID = "";
    for (var i = 0; i < GG.length; i++) {

        GUID += GG[i].toUpperCase();

    }

    return GUID;
}

function LimpaCNPJ_CPF()
{
    var val = document.getElementById("osc_cnpj_cpf");
    val.value = "";
}

