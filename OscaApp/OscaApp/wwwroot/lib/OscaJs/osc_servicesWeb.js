 
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

            $('#valorEmAberto').val(FormatMoneyCompleto(resp.valor));
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

            $('#valorEmAberto').val(FormatMoneyCompleto(resp.valor));
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

            $('#valorRecebido').val(FormatMoneyCompleto(resp.valor));
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

            $('#valorDiario').val(FormatMoneyCompleto(resp.valor));
        }

    });

}

function ValorDiarioDinheiro() {

    var url = '/API/BalcaoVendasAPI/RetornaValorVendaDiarioDinheiro';

    var valorVenda = 0;

    $.ajax({
        url: url,
        type: "GET",
        datatype: 'json',
        ContentType: 'application/json;utf-8'
    }).done(function (resp) {

        if (resp.statusOperation == true) {

            $('#valorDiarioDinheiro').val(FormatMoneyCompleto(resp.valor));
        }

    });

}