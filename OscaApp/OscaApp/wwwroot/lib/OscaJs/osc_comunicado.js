function OnLoad_Comunicado()
{
    //Configura calendario  
    Calendario("#osc_dataFim");
    Calendario("#osc_dataInicio");
}
function DeleteComunicado(id) {

    DeleteService(id, "ComunicadoAPI", "GridComunicado?view=0");
} 


 
