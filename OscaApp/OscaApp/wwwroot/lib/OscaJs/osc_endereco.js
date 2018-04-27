 

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

function ConsultaCep() {    

    var cepEntrada = $("#osc_cep").val();

        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/API/EnderecoAPI/ConsultaCep",
            data: { cep: cepEntrada },
            success: function (dados) {
                if (dados.statusOperation == true) {

                    $("#osc_bairro").val(dados.value.bairro);   
                    $("#osc_logradouro").val(dados.value.logradouro);   
                    $("#osc_cidade").val(dados.value.cidade);                                                      
                                         
                }
            }
        });    
} 

