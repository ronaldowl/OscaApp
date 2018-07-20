function OpenLookupCliente() {
    var idCliente = $("#osc_clienteId").val();

    if (idCliente != "00000000-0000-0000-0000-000000000000") {
        $(window.document.location).attr('href', "/Cliente/FormUpdateCliente?id=" + idCliente);
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