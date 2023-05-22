using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;

namespace ClienteTACOSWeb.Pages;

public class IniciarSesionModel : PageModel
{
    private readonly ILogger<IniciarSesionModel> _logger;
    public IConsultanteMgt _consultante {get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    [BindProperty(SupportsGet =true)]
    public string Email { get; set; }

    [BindProperty(SupportsGet =true)]
    public string Contrasena { get; set; }

    [BindProperty(SupportsGet =true)]
    public bool esLogin { get; set; }

    public IniciarSesionModel(ILogger<IniciarSesionModel> logger, 
                            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                            IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
        this.Email = "";
        this.Contrasena = "";
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
        RespuestaIniciarSesion datosSesion = this._consultante.IniciarSesion(persona);
        _logger.LogInformation($"Email: {datosSesion.Persona.Email}");
        _logger.LogInformation($"Contrasena: {datosSesion.Persona.Miembros.ElementAt(0).Contrasena}");
        _logger.LogInformation($"Respuesta: {datosSesion}");
        if (datosSesion != null)
        {
            Request.HttpContext.Session.SetString("Email", datosSesion.Persona.Email);
            Request.HttpContext.Session.SetString("Token", datosSesion.Token);
            Request.HttpContext.Session.SetString("Nombre", datosSesion.Persona.Nombre);
            Response.Redirect("Menu");


        }

    }
}