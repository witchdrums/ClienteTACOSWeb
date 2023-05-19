using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;

namespace ClienteTACOSWeb.Pages;

public class IniciarSesionModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IConsultanteMgt _consultante {get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    [BindProperty(SupportsGet =true)]
    public string Email { get; set; }

    [BindProperty(SupportsGet =true)]
    public string Contrasena { get; set; }

    [BindProperty(SupportsGet =true)]
    public bool esLogin { get; set; }

    public IniciarSesionModel(ILogger<IndexModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
    }

    public void OnPostEntrar()
    {
        PersonaModelo persona = new PersonaModelo{
                Miembros = new List<MiembroModelo> { 
                    new MiembroModelo()
                }
            };
        persona.Email = this.Email;
        persona.Miembros.ElementAt(0).Contrasena = this.Contrasena;
        this._consultante.IniciarSesion(persona);
    }
}