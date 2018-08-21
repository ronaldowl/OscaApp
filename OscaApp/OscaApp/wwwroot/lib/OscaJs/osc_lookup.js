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
