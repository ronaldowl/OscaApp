 
function ValorEmAbertoCliente(idCli) {

    var url = '/API/ContasReceberAPI/RetornaValorEmAbertoCliente';

    var valorVenda = 0;

    $.ajax({
        url: url,
        type: "GET",
        datatype: 'json',
        data: {
            id: idCli
        },
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $('#valorEmAberto').val(FormataDecimal(parseFloat(resp.valor), 2, ',', '.'));
        }

    });

}

function ValorEmAberto() {

    var url = '/API/ContasReceberAPI/RetornaValorEmAberto';

    var valorVenda = 0;

    $.ajax({
        url: url,
        type: "GET",
        datatype: 'json',
     
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $('#valorEmAberto').val(FormataDecimal(parseFloat(resp.valor), 2, ',', '.'));
        }

    });

}

function ValorRecebidoCliente(idCli) {

    var url = '/API/ContasReceberAPI/RetornaValorRecebidoCliente';

    var valorVenda = 0;

    $.ajax({
        url: url,
        type: "GET",
        datatype: 'json',
        data: {
            id: idCli
        },
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $('#valorRecebido').val(FormataDecimal(parseFloat(resp.valor), 2, ',', '.'));
        }

    });

}

function ValorDiario() {

    var url = '/API/BalcaoVendasAPI/RetornaValorVendaDiario';

    var valorVenda = 0;

    $.ajax({
        url: url,
        type: "GET",
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $('#valorDiario').val(FormataDecimal(parseFloat(resp.valor), 2, ',', '.'));
        }

    });

}