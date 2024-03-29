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

    [BindProperty(SupportsGet = true)]
    public AlimentoModelo AlimentoModificable { get; set; } = new AlimentoModelo();

    [BindProperty(SupportsGet = true)]
    public string NombreAlimento { get; set; } = "";
    [BindProperty(SupportsGet = true)]
    public string DescripcionAlimento { get; set; } = "";
    [BindProperty(SupportsGet = true)]
    public int PrecioAlimento { get; set; } = 0;
    [BindProperty(SupportsGet = true)]
    public int ExistenciaAlimento { get; set; } = 0;
    public Sesion sesion { private set; get; }

    public MenuModel(ILogger<MenuModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IMenuMgt menu,
                       IConsultanteMgt consultante,
                       Sesion sesion)
    {
        _logger = logger;
        _menu = menu;
        _consultante = consultante;
        _env = env;
        this.sesion = sesion;
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
        if (String.IsNullOrWhiteSpace(Request.HttpContext.Session.GetString("Token")))
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

    public void OnPostModificarAlimento()
    {
        //this.AlimentoModificable.Id = this.idCurso;
        this._menu.ModificarAlimento(this.AlimentoModificable);
        this.Response.Redirect("Menu");
    }
}