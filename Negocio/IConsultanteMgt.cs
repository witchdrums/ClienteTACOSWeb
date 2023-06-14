using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio.Peticiones;

namespace ClienteTACOSWeb.Negocio;
public interface IConsultanteMgt{
    public PedidoModelo PedidoSeleccionado { get; set; }    

    public PedidoModelo ObtenerPedido();
    public void AgregarAlimentoAPedido(AlimentoModelo alimento);

    public Credenciales IniciarSesion(PeticionCredenciales credenciales);

    public PersonaModelo ObtenerMiembroEnSesion();

    public List<PedidoModelo> ObtenerPedidos(DateTime desde, DateTime hasta);

    public PedidoModelo SeleccionarPedido(int IdPedido);
    
    public void ActualizarPedido(PedidoSimple pedido);

    public void RegistrarMiembro(MiembroModelo miembro);

    public Dictionary<int, int> CancelarPedido();

    public void RegistrarPedido();
    public List<ResenaModelo> ObtenerResenas();

    public HttpResponseMessage EliminarResena(int idResena);
}