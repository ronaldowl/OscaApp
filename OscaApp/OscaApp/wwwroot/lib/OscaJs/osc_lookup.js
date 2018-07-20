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