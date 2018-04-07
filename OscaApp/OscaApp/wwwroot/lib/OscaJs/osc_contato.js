//********************************* Biblioteca com funções da Tela de Contato***************

function DeleteContato(id) {

    DeleteService(id, "ContatoAPI", "GridContato?view=0");
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
            val.value = formatCPF(unFormatNumber(val.value));
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


function UpcaseForGuid(G) {

    var GG = G.split()
    var GUID = "";
    for (var i = 0; i < GG.length; i++) {

        GUID += GG[i].toUpperCase();

    }

    return GUID;
}

function LimpaCPF() {
    var val = document.getElementById("osc_cpf");
    val.value = "";
}