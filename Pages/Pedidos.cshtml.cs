using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using Microsoft.AspNetCore.Http;

namespace ClienteTACOSWeb.Pages;

public class PedidosModel : PageModel
{
    private readonly ILogger<PedidosModel> _logger;
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    public IConsultanteMgt _consultante {get; set; }
    public List<Estados> Estados {get; set; }
    [BindProperty(SupportsGet=true)]
    public int IdPedido {get; set; }
    public int IdEstado {get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime Desde { get; set; } = DateTime.Now.AddMonths(-1);
    [BindProperty(SupportsGet = true)]
    public DateTime Hasta { get; set; } = DateTime.Now;

    [BindProperty(SupportsGet = true)]
    public List<PedidoModelo> Pedidos { set; get; } = new List<PedidoModelo>();

    public PedidosModel(ILogger<PedidosModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        this._env = env;
        this.Estados = Enum.GetValues(typeof(Estados)).Cast<Estados>().ToList();
    }

    public void OnGet()
    {
        this.OnPostObtenerPedidosEntre();
    }
    
    public void OnPostActualizarPedido()
    {
        PedidoSimple pedido = 
            new PedidoSimple(this._consultante.SeleccionarPedido(this.IdPedido));
        pedido.Estado = this.IdEstado;
        this._consultante.ActualizarPedido(pedido);
    }

    public void OnPostObtenerPedidosEntre()
    {
        this.Pedidos.Clear();
        this.Pedidos = _consultante.ObtenerPedidos(this.Desde, this.Hasta);
    }
}