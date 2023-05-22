using ClienteTACOSWeb.Modelos;
namespace ClienteTACOSWeb.Negocio;
public interface IConsultanteMgt{
    
    public PedidoModelo ObtenerPedido();
    public void AgregarAlimentoAPedido(AlimentoModelo alimento);

    public RespuestaIniciarSesion IniciarSesion(PersonaModelo persona);

    public PersonaModelo ObtenerMiembroEnSesion();

    public List<PedidoModelo> ObtenerPedidos();

    public PedidoModelo ObtenerPedidoLocal(int IdPedido);
    
    public void ActualizarPedido(PedidoModelo pedido); 
}