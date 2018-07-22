﻿
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

                        inserirLinha(dados.listaProdutoBalcao[0].id, dados.listaProdutoBalcao[0].codigo, dados.listaProdutoBalcao[0].nome, dados.listaProdutoBalcao[0].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }), $('#osc_listaPreco').val())

                        SomaTotal();

                        LimpaBusca();

                    }
                    else {

                        $.each(dados.listaProdutoBalcao, function (idx, obj) {

                            $('#produtosPequisa').append('<tr id="key_' + dados.listaProdutoBalcao[idx].id + '" ><td style="width:20px"> <i class="fa fa-cubes"></i></td><td style="width:8%">' + dados.listaProdutoBalcao[idx].codigo + '</td><td> ' + dados.listaProdutoBalcao[idx].nome + '</td><td>' + dados.listaProdutoBalcao[idx].fabricante + '</td><td>' + dados.listaProdutoBalcao[idx].modelo + '</td><td style="width:7%">' + dados.listaProdutoBalcao[idx].quantidade + '</td><td>' + dados.listaProdutoBalcao[idx].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }) + '</td><td style="width:45px"> <div class="form-group col-md-2"><button id="osc_edit" type="button" class="btn btn-success btn-sm fa fa-edit" value="voltar"  onclick="AdicionaProduto(' + "'" + dados.listaProdutoBalcao[idx].id + "'" + ');"> <span>Adicionar..</span></button></div> </td></tr>');

                        });
                    }
                }
            }
        });
    }
}

function AdicionaProduto(idItemListaPreco) {

    var chaveLinha = "#key_" + idItemListaPreco + " td";
    var listaPreco = $('#osc_listaPreco').val();
    var TdCodigo = "";
    var TdProduto = "";
    var TdValor = 0;

    $(chaveLinha).each(function (index, value) {

        if (index == 1) TdCodigo = $(this).text();

        if (index == 2) TdProduto = $(this).text();

        if (index == 6) TdValor = $(this).text();

    }).fadeOut();

    inserirLinha(idItemListaPreco, TdCodigo, TdProduto, TdValor, listaPreco);

    SomaTotal();

    LimpaBusca();
}

function inserirLinha(idItemListaPreco, codigo, NomeProduto, valorProduto, lista) {
    html = '<tr id="keyPro_' + idItemListaPreco + '" class="item">'
        + '<td   id="osc_KeyProdCodigo_' + idItemListaPreco + '" ><input type="text"  class="form-control "  id="osc_ProdCodigo"   value="' + codigo + '" min="1" readonly/><input type="hidden" value="' + lista + '" class="lista"/><input type="hidden" value="' + idItemListaPreco + '" class="idItemListaPreco"/></td>'
        + '<td   id="osc_KeyProdNome_' + idItemListaPreco + '"   ><input type="text"  class="form-control "  id="osc_ProdNome"       value="' + NomeProduto + '" readonly /></td>'
        + '<td   id="osc_KeyProdQtd_' + idItemListaPreco + '"    ><input class="qtd"   type="number" class="form-control"  id="osc_ProdQtd_' + idItemListaPreco + '"        value="1" min="1" onchange="calcularLinha(' + "'" + idItemListaPreco + "'" + ')" /></td>'
        + '<td   id="osc_KeyProdValor_' + idItemListaPreco + '"  ><input class="valor" type="text"   class="form-control"  id="osc_ProdValor_' + idItemListaPreco + '"      value="' + valorProduto + '" readonly /></td>'
        + '<td   id="osc_KeyProdTotal_' + idItemListaPreco + '"  ><input class="somaTD" type="text" class="form-control"  id="osc_ProdTotal_' + idItemListaPreco + '"       value=" ' + valorProduto + '" readonly /></td>'
        + '<td   id="osc_acaoRemover_' + idItemListaPreco + '"   ><button type="button" class="btn btn-danger btn-md fa fa-remove"   onclick="RemoveLinhaProduto(' + "'" + idItemListaPreco + "'" + ');"> Remover..</button></td>'
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

    VALOR = VALOR.replace('R', '').replace('$', '');
    VALOR = parseFloat(VALOR.replace(',', '.'));

    TOTAL = QTD * VALOR;

    $(chaveTOTAL).val(FormatMoney(TOTAL));

    SomaTotal();
}

function SomaTotal() {

    var VALORTOTAL = 0;
    var totalDesconto = 0;
    var tipoDesconto = $('#osc_tipoDesconto').val();
    var valorDesconto = $('#osc_valorDesconto').val();

    $('#produtosVendas > tbody tr .somaTD').each(function (i) {

    

        VALORTOTAL += parseFloat( $(this).val().replace('$', '').replace('R', '').replace(',', '.'));
    });

    if (tipoDesconto == 1) {

        if (valorDesconto != undefined || valorDesconto > 0) {

            VALORTOTAL = VALORTOTAL - valorDesconto;
        }

    } else {

        if (valorDesconto != undefined || valorDesconto > 0)
        {
            totalDesconto = (VALORTOTAL / 100) * valorDesconto;
            VALORTOTAL = VALORTOTAL - totalDesconto;
        }
    }

   

    $('#ValorTotalVendas').val('TOTAL: ' + FormatMoney(VALORTOTAL));
    $('#ValorTotalVendasDiag').val('TOTAL: ' + FormatMoney(VALORTOTAL));

    $('#InputValorTotalVendas').val(VALORTOTAL);

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


    return ObjetoEntrada;
}

function MontaObjetoCliente() {

    var ObjetoCliente = new Object();

    ObjetoCliente.id = $('#osc_clienteId').val();
    ObjetoCliente.nomeCliente = $('#osc_clienteIdName').val();
    ObjetoCliente.tipoPessoa = $('#osc_tipopessoa').val();
    ObjetoCliente.email = $('#osc_email').val();
    ObjetoCliente.telefone = $('#osc_telefone').val();
    ObjetoCliente.cnpj_cpf = $('#osc_cnpj_cpf').val();

    return ObjetoCliente;
}

function MontaListaObjeto() {

    var todos = [];

    $('#produtosVendas > tbody tr').each(function (index, object) {

        var entidade = {
            idItemListaPreco: $(this).find('.idItemListaPreco').val(),
            quantidade: $(this).find('.qtd').val(),
            idListaPreco: $(this).find('.lista').val(),
            valor: $(this).find('.valor').val().replace('$', '').replace('R', '').replace('.', ''),
            valorTotal: $(this).find('.somaTD').val().replace('$', '').replace('R', '').replace('.', '')
        };

        todos.push(entidade);
    });

    return todos;
}

function CalcularParcela() {

    var valorTotal = $('#InputValorTotalVendas').val();
    var parcelas = $('#osc_parcelas').val();
    var calcResult = 0;

    calcResult = valorTotal / parcelas;

    $('#osc_valorParcela').val(FormatMoney(calcResult));
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
