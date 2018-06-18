function OnLoad_ListaPreco()
{

    //Configura calendario  
    Calendario("#osc_dataValidade");
    Calendario("#osc_fimValidade");
}

function ConsultaSeExisteListaPadrao() {
    var retorno = true;

    var lista = $("#osc_listaPrecoPadrao").val();
    alert(lista);

    if (lista == true) {
        $.ajax({
            dataType: "json",
            type: "GET",
            async: false,
            url: "/API/ListaPrecoAPI/ConsultaListaPrecoPadrao",
            data: { valor: null },
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
