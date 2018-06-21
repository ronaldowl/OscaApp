function OnLoad_ListaPreco()
{

    //Configura calendario  
    Calendario("#osc_dataValidade");
    Calendario("#osc_fimValidade");
}

function ConsultaSeExisteListaPadrao() {
    var retorno = true;

    var lista = document.getElementById("osc_listaPrecoPadrao").checked;
    var id = $("#osc_id").val();       

    if (lista == true) {
        $.ajax({
            dataType: "json",
            type: "GET",
            async: false,
            url: "/API/ListaPrecoAPI/ConsultaListaPrecoPadrao",
            data: { valor: id },
            success: function (dados) {
                if (dados.statusOperation == true) {
                    alert('Já existe uma lista de Preço Padrão!');
                    retorno = false;
                }
            }
        });
    }
  
    return retorno;
}  
