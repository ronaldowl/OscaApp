//********************************* Biblioteca com funções da Tela Produto do Pedido ***************

function Onchage_TipoDesconto() {

    var tipo = document.getElementById("osc_tipoDesconto");  
    var descontoValor = document.getElementById("osc_valorDescontoMoney");
    var descontoPercentual = document.getElementById("osc_valorDescontoPercentual");
    
    if (tipo.value == 1) {
       
        descontoValor.hidden = "false";
        descontoPercentual.hidden = "true";

    }

    if (tipo.value == 2) {
    
        descontoValor.hidden = "true";      
        descontoPercentual.hidden = "false";
    }
}


