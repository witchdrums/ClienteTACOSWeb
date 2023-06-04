using ClienteTACOSWeb.Modelos;
using System.Collections.ObjectModel;

namespace ClienteTACOSWeb.Negocio;

public sealed class Sesion
{
    public Credenciales Credenciales { get; set; } = new Credenciales();
    public bool MiembroConfirmado => Credenciales.Miembro.CodigoConfirmacion == 0;
    public ObservableCollection<AlimentoPedidoModelo> AlimentosPedidos { get; set; } =
        new ObservableCollection<AlimentoPedidoModelo>();
}
