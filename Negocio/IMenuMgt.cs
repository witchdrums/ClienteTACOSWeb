using System.Collections.ObjectModel;
using ClienteTACOSWeb.Modelos;

public interface IMenuMgt{
    public ObservableCollection<AlimentoModelo> ObtenerMenu();
    public AlimentoModelo ObtenerAlimento(int idAlimento);
}