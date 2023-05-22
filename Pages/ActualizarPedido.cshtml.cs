using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using Microsoft.AspNetCore.Http;

namespace ClienteTACOSWeb.Pages;

public class ActualizarPedidoModel : PageModel
{
    public IConsultanteMgt _consultante {get; set; }
    private readonly ILogger<MenuModel> _logger;
    public List<AlimentoModelo> _pedido {get; set; } = new List<AlimentoModelo>();
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    [BindProperty(SupportsGet=true)]
    public int idPedido {get; set;}
    public PedidoModelo? Pedido {get; set;}
    public List<Estados> Estados {get; set; }
    
    [BindProperty(SupportsGet=true)]
    public int IdEstado {get; set;}
    public ActualizarPedidoModel(ILogger<MenuModel> logger, 
                                    Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                                    IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
        this.Estados = Enum.GetValues(typeof(Estados)).Cast<Estados>().ToList();

    }

    public void OnGet()
    {
        this.Pedido = _consultante.ObtenerPedidoLocal(this.idPedido);
    }

    public void OnPostActualizarPedido()
    {
        Console.WriteLine(this.IdEstado);
    }
}
