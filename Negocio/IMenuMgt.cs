using ClienteTACOSWeb.Modelos;

public interface IMenuMgt{
    public List<AlimentoModelo> ObtenerMenu();
    public AlimentoModelo ObtenerAlimento(int idAlimento);
}