﻿//********************************* Biblioteca com funções da Tela Produto do Pedido ***************

function OnLoad_ServicoOrdem() {
    Onchage_ServicoOrdemTipoDesconto();
}

function Onchage_ServicoOrdemTipoDesconto() {
   
    var tipo = document.getElementById("osc_tipoDesconto");
    var descontoValor = document.getElementById("osc_valorDescontoMoney");
    var descontoPercentual = document.getElementById("osc_valorDescontoPercentual");

    if (tipo.value == 1) {

        $("#osc_valorDescontoPercentual").prop('disabled', true);
        $("#osc_valorDescontoMoney").prop('disabled', false);
        descontoPercentual.value = "0";

    }

    if (tipo.value == 2) {

        $("#osc_valorDescontoPercentual").prop('disabled', false);
        $("#osc_valorDescontoMoney").prop('disabled', true);
        descontoValor.value = "0";

    }
}





