 

function DeleteEndereco(id, idcliente) {



    if (confirm("Deseja Excluir o Registro?")) {
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/API/EnderecoAPI/Delete",
            data: { id: id },
            success: function (dados) {
                if (dados.statusOperation == true) {
                    alert('Registro excluido com Sucesso!');
                    $(window.document.location).attr('href', 'GridEndereco?idCliente=' + idcliente);
                }
            }
        });
    }
} 
