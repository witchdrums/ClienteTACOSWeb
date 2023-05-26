using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;

namespace ClienteTACOSWeb.Pages;

public class IniciarSesionModel : PageModel
{
    private readonly ILogger<IniciarSesionModel> _logger;
    public IConsultanteMgt _consultante { get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;
    [BindProperty(SupportsGet = true)]

    [Required(ErrorMessage = "No puede estar vacío")]
    //[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; }

    [Required(ErrorMessage = "No puede estar vacío")]
    //[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas deben coincidir")]
    [BindProperty(SupportsGet = true)]
    public string ContrasenaConfirmacion { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool esLogin { get; set; }

    [BindProperty(SupportsGet = true)]
    public PersonaModelo Persona { set; get; }

    [BindProperty(SupportsGet = true)]
    public bool Error { get; set; }

    [BindProperty(SupportsGet = true)]
    public string MensajeError { get; set; } = "Sin mensaje";


    public IniciarSesionModel(ILogger<IniciarSesionModel> logger, 
                            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                            IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
        this.Persona = new PersonaModelo();
    }

    public void OnPostEntrar()
    {
        try
        {
            this.Persona.Miembros.ElementAt(0).Contrasena = this.Contrasena;
            RespuestaIniciarSesion datosSesion = this._consultante.IniciarSesion(this.Persona);
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
        catch (Exception e)
        {
            this.Error = true;
            //Response.Redirect("Error");
            this.MensajeError = e.Message;
        }
    }

    public async void OnPostUnirse()
    {
        ModelState.Remove("MensajeError");

        var errors = ModelState.Values.SelectMany(v => v.Errors);

        if (TryValidateModel(this.Persona, nameof(this.Persona)))
        {
            try
            {
                this.Persona.Miembros.ElementAt(0).Contrasena = this.Contrasena;
                await this._consultante.RegistrarMiembro(Persona);
            }
            catch (Exception a)
            {
                this.MostrarError(a.Message);
            }
        }
        else
        {
            this.MostrarError("no lol");
        }
    }

    private void MostrarError(string mensaje)
    {
        this.Error = true;
        this.MensajeError = "Nope lol";
    }
}