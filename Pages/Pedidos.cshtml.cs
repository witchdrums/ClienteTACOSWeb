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
    public PedidosModel(ILogger<PedidosModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        this._env = env;
        this.Estados = Enum.GetValues(typeof(Estados)).Cast<Estados>().ToList();
    }
    
    public void OnPostActualizarPedido()
    {
        PedidoModelo pedido = this._consultante.ObtenerPedidoLocal(this.IdPedido);
        pedido.Estado = this.IdEstado;
        this._consultante.ActualizarPedido(pedido);
    }


}