using ClienteTACOSWeb.Modelos;
namespace ClienteTACOSWeb.Negocio;
public interface IConsultanteMgt{
    
    public PedidoModelo ObtenerPedido();
    public void AgregarAlimentoAPedido(AlimentoModelo alimento);

    public void IniciarSesion(PersonaModelo persona);

    public PersonaModelo ObtenerMiembroEnSesion();
}