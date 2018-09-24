function OpenLookupCliente() {
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
    }
} 

function OpenLookupEndereco(idCliente) {

    var idEndereco = "00000000-0000-0000-0000-000000000000";
    idEndereco = $("#osc_enderecoId").val();

    if (idEndereco != "00000000-0000-0000-0000-000000000000" ) {
        $(window.document.location).attr('href', "/Endereco/FormUpdateEndereco?id=" + idEndereco);
    } else {
        $(window.document.location).attr('href', "/Endereco/FormCreateEndereco?idCliente=" + idCliente);
    }
}

function OpenLookupEndereco2(idCliente) {
    var idEndereco = "00000000-0000-0000-0000-000000000000";
    idEndereco = $("#osc_enderecoId2").val();

    if (idEndereco != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Endereco/FormUpdateEndereco?id=" + idEndereco);
    } else {
        $(window.document.location).attr('href', "/Endereco/FormCreateEndereco?idCliente=" + idCliente);
    }
}

function OpenLookupProfissional() {
    var id = $("#osc_IdProfissional").val();

    if (id != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Profissional/FormUpdateProfissional?id=" + id);
    }
}  


function CarregaLookupCliente() {
    $("#lookupCliente").load("/Cliente/LookupCliente", function () {
        $("#lookupCliente").modal("show");
    })
}

function CarregaUpdateProdutoOrdem(id) {
    $("#ModalProdutoOrdem").load("/ProdutoOrdem/FormUpdateProdutoOrdem?id=" + id, function () {
        $("#ModalProdutoOrdem").modal("show");
    });
}

function CarregaLookupProfissional() {

    $("#lookupProfissional").load("/Profissional/LookupProfissional", function () {
        $("#lookupProfissional").modal("show");
    });
}

function CarregaLookupCategoriaManutencao() {
    $("#lookupCategoriaManutencao").load("/CategoriaManutencao/LookupCategoriaManutencao", function () {
        $("#lookupCategoriaManutencao").modal("show");
    })
}

function CarregaUpdateServicoOrdem(id) {
    $("#ModalServicoOrdem").load("/ServicoOrdem/FormUpdateServicoOrdem?id=" + id, function () {
        $("#ModalServicoOrdem").modal("show");
    });
}

function CarregaLookupEndereco(id) {
    $("#lookupEndereco").load("/Endereco/LookupEndereco?idCliente=" + id, function () {
        $("#lookupEndereco").modal("show");
    })
}

function CarregaLookupEndereco2(id) {
    $("#lookupEndereco2").load("/Endereco/LookupEndereco2?idCliente=" + id, function () {
        $("#lookupEndereco2").modal("show");
    })
}

function CarregaLookupFornecedor() {
    $("#lookupFornecedor").load("/Fornecedor/LookupFornecedor", function () {
        $("#lookupFornecedor").modal("show");
    })
}

function ConfigLookupFornecedor(id, name) {

    $("#osc_fornecedorIdName").val(name);
    $("#osc_fornecedorId").val(id);  

    $('#lookupFornecedor').modal('hide');
}

function OpenLookupFornecedor() {
    var id = $("#osc_fornecedorId").val();

    if (id != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Fornecedor/FormUpdateFornecedor?id=" + id);
    }
}  

function CarregaLookupBalcaoVendas() {
    $("#lookupBalcaoVendas").load("/BalcaoVendas/LookupBalcaoVendas", function () {
        $("#lookupBalcaoVendas").modal("show");
    })
}

function OpenLookupBalcaoVendas(idBalcaoVendas) { 

    if (idBalcaoVendas != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/BalcaoVendas/BalcaoVendasView?id=" + idBalcaoVendas);
    }
} 

function CarregaLookupPedido() {

    $("#lookupPedido").load("/pedido/LookupPedido", function () {
        $("#lookupPedido").modal("show");
    });
}

function CarregaLookupOS() {

    $("#lookupOS").load("/OrdemServico/LookupOrdemServico", function () {
        $("#lookupOS").modal("show");
    });
}

function CarregaLookupAtendimento() {
    $("#lookupAtendimento").load("/Atendimento/LookupAtendimento", function () {
        $("#lookupAtendimento").modal("show");
    })
}

function CarregaMultiploLookup(){        

    var tipo = document.getElementById("osc_origemContaReceber").value; 
    
    if (tipo == 0) {
        CarregaLookupPedido();
    }

    if (tipo == 1) {
        CarregaLookupAtendimento();
    }

    if (tipo == 2) {
        CarregaLookupOrdemServico();
    }

    if (tipo == 3) {
        CarregaLookupBalcaoVendas();
    }
}

function OpenMultiploLookup() {

    var tipo = document.getElementById("osc_origemContaReceber").value;
    var idReferencia = document.getElementById("osc_referenceId").value;
    
    if (tipo == 0) {
        CarregaLookupPedido();
    }

    if (tipo == 1) {
        CarregaLookupAtendimento();
    }

    if (tipo == 2) {
        CarregaLookupOrdemServico();
    }

    if (tipo == 3) {
        OpenLookupBalcaoVendas(idReferencia);
    }
}