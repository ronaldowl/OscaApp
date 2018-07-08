//********************************* Biblioteca com funções da Tela de Contato***************

function DeleteContato(id) {

    DeleteService(id, "ContatoAPI", "GridContato?view=0");
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