using System.Collections.ObjectModel;
using ClienteTACOSWeb.Modelos;

public interface IMenuMgt{
    public Task CargarMenu();
    public List<AlimentoModelo> ObtenerMenu();
    public AlimentoModelo ObtenerAlimento(int idAlimento);
    public void ActualizarExistenciaAlimentos(Dictionary<int,int> idAlimentos_Cantidades);

    public void ModificarAlimento(AlimentoModelo alimento);
}