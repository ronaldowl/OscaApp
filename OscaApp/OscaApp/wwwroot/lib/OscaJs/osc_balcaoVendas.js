 

function ConsultaProduto(evento) {

    if (evento.keyCode != 13) return false;


    var lista = $("#osc_listaPreco").val();
    var filtroProduto = $("#osc_pesquisaProduto").val();

    $("#produtosPequisa tr:has(td)").remove();

    if (filtroProduto != "") {

        $.ajax({
            dataType: "json",
            type: "GET",
            async: false,
            url: "/API/BalcaoVendasAPI/ConsultaProduto",
            data: {
                filtro: filtroProduto,
                idLista: lista
            },
            success: function (dados) {

                if (dados.statusOperation == true) {

                    //Caso traga apenas um produto adiciona direto na lista
                    if (dados.listaProdutoBalcao.length == 1) {

                        inserirLinha(dados.listaProdutoBalcao[0].id, dados.listaProdutoBalcao[0].codigo, dados.listaProdutoBalcao[0].nome, FormataDecimal(dados.listaProdutoBalcao[0].valor, 2, ',', '.'), $('#osc_listaPreco').val(), dados.listaProdutoBalcao[0].idItemListaPreco);

                        SomaTotal();

                        LimpaBusca();

                    }
                    else {

                        $.each(dados.listaProdutoBalcao, function (idx, obj) {

                            $('#produtosPequisa').append('<tr id="key_' + dados.listaProdutoBalcao[idx].id + '" ><td style="width:20px"> <i class="fa fa-cubes"></i></td><td style="width:8%">' + dados.listaProdutoBalcao[idx].codigo + '</td><td> ' + dados.listaProdutoBalcao[idx].nome + '</td><td>' + dados.listaProdutoBalcao[idx].fabricante + '</td><td>' + dados.listaProdutoBalcao[idx].modelo + '</td><td style="width:7%">' + dados.listaProdutoBalcao[idx].quantidade + '</td><td>' + FormataDecimal(dados.listaProdutoBalcao[idx].valor, 2, ',', '.') + '</td><td style="width:45px"> <div class="form-group col-md-2"><button id="osc_edit" type="button" class="btn btn-success btn-sm fa fa-edit" value="voltar"  onclick="AdicionaProduto(' + "'" + dados.listaProdutoBalcao[idx].id + "','" + dados.listaProdutoBalcao[idx].idItemListaPreco + "');" + '"' + '""><span>Adicionar..</span></button></div> </td></tr>');

                        });
                    }
                }
            }
        });
    }
}

function AdicionaProduto(idProduto, idItemLista ) {

    var chaveLinha = "#key_" + idProduto + " td";
    
    var listaPreco = $('#osc_listaPreco').val();
    var TdCodigo = "";
    var TdProduto = "";
    var TdValor = 0;

    $(chaveLinha).each(function (index, value) {

        if (index == 1) TdCodigo = $(this).text();

        if (index == 2) TdProduto = $(this).text();

        if (index == 6) TdValor = $(this).text();

    }).fadeOut();

    inserirLinha(idProduto, TdCodigo, TdProduto, TdValor, listaPreco, idItemLista);

    SomaTotal();

    LimpaBusca();
}

function inserirLinha(idProduto, codigo, NomeProduto, valorProduto, lista, idItemLista) {
    html = '<tr id="keyPro_' + idProduto + '" class="item">'
        + '<td   id="osc_KeyProdCodigo_' + idProduto + '" ><input type="text"  class="form-control "  id="osc_ProdCodigo"   value="' + codigo + '" min="1" readonly/><input type="hidden" value="' + lista + '" class="lista"/><input type="hidden" value="' + idProduto + '" class="idProduto"/><input type="hidden" value="' + lista + '" class="lista"/><input type="hidden" value="' + idItemLista + '" class="idItemListaPreco"/></td>'
        + '<td   id="osc_KeyProdNome_' + idProduto + '"   ><input type="text"  class="form-control "  id="osc_ProdNome"       value="' + NomeProduto + '" readonly /></td>'
        + '<td   id="osc_KeyProdQtd_' + idProduto + '"    ><input class="qtd"   type="number" class="form-control"  id="osc_ProdQtd_' + idProduto + '"        value="1" min="1" onchange="calcularLinha(' + "'" + idProduto + "'" + ')" /></td>'
        + '<td   id="osc_KeyProdValor_' + idProduto + '"  ><input class="valor" type="text"   class="form-control"  id="osc_ProdValor_' + idProduto + '"      value="' + valorProduto + '"onblur="onBlur(this)" onchange="calcularLinha(' + "'" + idProduto + "'" + ')" /></td>'
        + '<td   id="osc_KeyProdTotal_' + idProduto + '"  ><input class="somaTD" type="text" class="form-control"  id="osc_ProdTotal_' + idProduto + '"       value=" ' + valorProduto + '" disabled="disabled" /></td>'
        + '<td   id="osc_acaoRemover_' + idProduto + '"   ><button type="button" class="btn btn-danger btn-md fa fa-remove"   onclick="RemoveLinhaProduto(' + "'" + idProduto + "'" + ');"> Remover..</button></td>'
        + '</tr>';
    $('#produtosVendas').append(html);
};

function RemoveLinhaProduto(linha) {

    var chaveTr = "#keyPro_" + linha;
    $(chaveTr).remove();
    SomaTotal();

}
function calcularLinha(chaveLinha) {

    var chaveLinhaCal = "#keyPro_" + chaveLinha;
    var chaveQTD = "#osc_ProdQtd_" + chaveLinha;
    var chaveVALOR = "#osc_ProdValor_" + chaveLinha;
    var chaveTOTAL = "#osc_ProdTotal_" + chaveLinha;
    var TOTAL = "";

    var QTD = $(chaveQTD).val();
    var VALOR = $(chaveVALOR).val();
     
    VALOR = parseFloat(PrepCalcDecimal(VALOR));

    TOTAL = QTD * VALOR;

    $(chaveTOTAL).val(FormataDecimal(TOTAL));

    SomaTotal();
}

function SomaTotal() {

    var VALORTOTAL = 0;
    var totalDesconto = 0;
    var tipoDesconto = $('#osc_tipoDesconto').val();
    var valorDesconto = $('#osc_valorDesconto').val();
    valorDesconto = parseFloat(PrepCalcDecimal(valorDesconto));

    $('#produtosVendas > tbody tr .somaTD').each(function (i) {  
                
        VALORTOTAL   += parseFloat(PrepCalcDecimal($(this).val()));
    });

    if (tipoDesconto == 1 & valorDesconto > 0) {

        if (valorDesconto != undefined ) {

            VALORTOTAL = VALORTOTAL - valorDesconto;
        }

    } else {

        if (valorDesconto != undefined & valorDesconto > 0)
        {
            totalDesconto = (VALORTOTAL / 100) * valorDesconto;
            VALORTOTAL = VALORTOTAL - totalDesconto;
        }
    }   

    $('#ValorTotalVendas').val('TOTAL: ' + FormataDecimal(VALORTOTAL, 2, ',', '.'));
    $('#ValorTotalVendasDiag').val('TOTAL: ' + FormataDecimal(VALORTOTAL, 2, ',', '.'));
    $('#InputValorTotalVendas').val(FormataDecimal(VALORTOTAL));
}

function LimpaBusca() {

    var chaveBusca = '#osc_pesquisaProduto';
    //Limpa Pequisa
    $("#produtosPequisa tr:has(td)").remove();
    $(chaveBusca).val('');
}


function Execute() {

    var url = '/API/BalcaoVendasAPI/GravarVenda';
    var produtosBalcaoP = MontaListaObjeto();
    var Entrada = MontaObjetoEntrada();
    var entradaCliente = MontaObjetoCliente();

    $.ajax({
        url: url,
        type: "POST",
        data: {
            modelo: Entrada,
            produtosBalcao: produtosBalcaoP,
            cliente: entradaCliente
        },
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $(window.document.location).attr('href', 'BalcaoVendasView?id=' + resp.id);
        } else {
            alert('Falha ao atualizar! - ' + dados.statusMensagem);
        }

    }).error(function (err) {
        //alert("Error " + err.status);
    });

}

function MontaObjetoEntrada() {

    var ObjetoEntrada = new Object();

    ObjetoEntrada.valorTotal = $('#InputValorTotalVendas').val();
    ObjetoEntrada.idListaPreco = $('#osc_listaPreco').val();
    ObjetoEntrada.cpf = $('#osc_cnpj_cpf').val();
    ObjetoEntrada.condicaoPagamento = $('#osc_condicaoPagamento').val();
    ObjetoEntrada.tipoPagamento = $('#osc_tipoPagamento').val();
    ObjetoEntrada.parcelas = $('#osc_parcelas').val();
    ObjetoEntrada.diaVencimento = $('#osc_diaVencimento').val();
    ObjetoEntrada.valorDesconto = $('#osc_valorDesconto').val();
    ObjetoEntrada.tipoDesconto = $('#osc_tipoDesconto').val();
    ObjetoEntrada.troco = $('#osc_troco').val();
    ObjetoEntrada.valorDinheiroPago = $('#osc_valorDinheiroPago').val();
     
    return ObjetoEntrada;
}

function MontaObjetoCliente() {

    var ObjetoCliente = new Object();

    ObjetoCliente.id = $('#osc_clienteId').val();
    ObjetoCliente.nomeCliente = $('#osc_clienteIdName').val();
    ObjetoCliente.tipoPessoa = $('#osc_tipopessoa').val();
    ObjetoCliente.email = $('#osc_clienteEmail').val();
    ObjetoCliente.telefone = $('#osc_clienteTelefone').val();
    ObjetoCliente.cnpj_cpf = $('#osc_cnpj_cpf').val();

    return ObjetoCliente;
}

function MontaListaObjeto() {

    var todos = [];

    $('#produtosVendas > tbody tr').each(function (index, object) {

        var entidade = {
            idProduto: $(this).find('.idProduto').val(),
            idItemListaPreco: $(this).find('.idItemListaPreco').val(),
            quantidade: $(this).find('.qtd').val(),
            idListaPreco: $(this).find('.lista').val(),
            valor: $(this).find('.valor').val(),
            valorTotal: $(this).find('.somaTD').val()
        };

        todos.push(entidade);
    });

    return todos;
}

function CalcularParcela() {

    var valorTotal = $('#InputValorTotalVendas').val();
    var parcelas = $('#osc_parcelas').val();
    var calcResult = 0;

    valorTotal = parseFloat(PrepCalcDecimal(valorTotal));


    calcResult = valorTotal / parcelas;

    $('#osc_valorParcela').val(FormataDecimal(calcResult));
}
function HabilitaTroco() {

    var tipoPagamento = document.getElementById("osc_tipoPagamento");
    var condicaoPagamento = document.getElementById("osc_condicaoPagamento");


    if (tipoPagamento.value == 1 & condicaoPagamento.value == 1) {

        document.getElementById("div_troco").hidden = false;
        document.getElementById("div_valorDinheiro").hidden = false;
      
        $("#osc_valorDinheiro").att('required', 'required');      
        
    } else {

        document.getElementById("div_troco").hidden = true;
        document.getElementById("div_valorDinheiro").hidden = true;

        $("#osc_troco").val('0,00');
        $("#osc_valorDinheiro").val('0,00');
    }
}

function HabilitaParcela() {
 
    var condPag = document.getElementById("osc_condicaoPagamento");    


    if (condPag.value == 3) {

        document.getElementById("div_Parcela").hidden = false;
        document.getElementById("div_DiaVencimento").hidden = false;
        document.getElementById("div_valorParcela").hidden = false;

    } else {

        document.getElementById("div_Parcela").hidden = true;
        document.getElementById("div_DiaVencimento").hidden = true;
        document.getElementById("div_valorParcela").hidden = true;

        $("#osc_parcelas").val('1');
        $("#osc_diaVencimento").val('1');
        $("#osc_valorParcela").val('R$ 0,00');

    }
}

function ValidaProdutoVenda() {

    var mensagem = true;

    $('#produtosVendas > tbody tr .somaTD').each(function (i) {

        mensagem = false;
    });

    if (mensagem) {
        alert('Por favor, adicione os produtos primeiro, antes de fechar a venda.');
        return false;
    } else { return true}
}

function BalcaoViewOnload() {

    var status = document.getElementById("osc_statusBalcaoVendas").value;
    desabilitaCampos_BalcaoView(status)

}

function desabilitaCampos_BalcaoView(status) {

    if (status == 1 || status == 2) {

        //Desabilita todos campos do Form
        $("input,select, textarea").prop("disabled", true);

        //Esconde botoes
        $("#osc_salvar").hide();

        $("#botaoLookupCliente").hide();  

    } else {

        //Desabilita botões
        //Esconde botoes
        $("#osc_reabrir").hide();

    }
}

function CalculaTroco() {

    var valorVenda = $('#InputValorTotalVendas').val();
    valorVenda = parseFloat(PrepCalcDecimal(valorVenda));

    var valorDinheiro = $('#osc_valorDinheiroPago').val();
    valorDinheiro = parseFloat(PrepCalcDecimal(valorDinheiro));

    var troco = valorDinheiro - valorVenda;
    
    $('#osc_troco').val(FormataDecimal(troco));

}

function limpaTroco() {
    $('#osc_troco').val();
    $('#osc_valorDinheiroPago').val();
}

function ValidaValorDinheiro() {

    var ev = $('#osc_valorDinheiroPago').val();

    if (ev == '' || ev == undefined) {
        alert('Favor preecher o valor em dinheiro');
        return false;
    }

    var valorVenda = $('#InputValorTotalVendas').val();
    valorVenda = parseFloat(PrepCalcDecimal(valorVenda));

    ev = parseFloat(PrepCalcDecimal(ev));

    if (ev < valorVenda) {

        alert("O valor pago é menor que o valor da venda!");
        return false;
    }

    return true;
}
