using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using Microsoft.AspNetCore.Http;

namespace ClienteTACOSWeb.Pages;

public class MenuModel : PageModel
{
    private readonly ILogger<MenuModel> _logger;
    public IMenuMgt _menu {get; set; }
    public IConsultanteMgt _consultante {get; set; }
    public List<AlimentoModelo> _pedido {get; set; } = new List<AlimentoModelo>();
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    [BindProperty(SupportsGet=true)]
    public int idCurso {get; set;}

    public MenuModel(ILogger<MenuModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IMenuMgt menu,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _menu = menu;
        _consultante = consultante;
        _env = env;
    }

    public async void OnGet()
    {
        await this._menu.CargarMenu();
    }

    public void OnPostAgregarAlimentoAPedido()
    {
        AlimentoModelo alimento = this._menu.ObtenerAlimento(this.idCurso);
        this._consultante.AgregarAlimentoAPedido(alimento);
        this._menu.ActualizarExistenciaAlimentos(new Dictionary<int, int> { { alimento.Id, -1 } });
        //this._pedido.Add(alimento);
    }

    public void OnPostOrdenar()
    {
        if (Request.HttpContext.Session.GetString("Token") is null)
        {
            Response.Redirect("IniciarSesion");
        }else
        {
            this._consultante.RegistrarPedido();
        }
        
    }

    public void OnPostCancelar()
    {
        this._menu.ActualizarExistenciaAlimentos(this._consultante.CancelarPedido());
    }
}