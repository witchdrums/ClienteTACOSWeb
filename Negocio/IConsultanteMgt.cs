using ClienteTACOSWeb.Modelos;
namespace ClienteTACOSWeb.Negocio;
public interface IConsultanteMgt{
    public string Token { set; get; }

    public PedidoModelo ObtenerPedido();
    public void AgregarAlimentoAPedido(AlimentoModelo alimento);

    public Credenciales IniciarSesion(PersonaModelo persona);

    public PersonaModelo ObtenerMiembroEnSesion();

    public List<PedidoModelo> ObtenerPedidos(DateTime desde, DateTime hasta);

    public PedidoModelo ObtenerPedidoLocal(int IdPedido);
    
    public void ActualizarPedido(PedidoSimple pedido);

    public Task RegistrarMiembro(PersonaModelo persona);

    public Dictionary<int, int> CancelarPedido();

    public void RegistrarPedido();
    public List<ResenaModelo> ObtenerResenas();

    public HttpResponseMessage EliminarResena(int idResena);
}