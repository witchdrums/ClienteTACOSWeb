using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;

namespace ClienteTACOSWeb.Pages;

public class IniciarSesionModel : PageModel
{
    private readonly ILogger<IniciarSesionModel> logger;
    public IConsultanteMgt consultante { get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment env;
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

    private Sesion sesion;
    public IniciarSesionModel(ILogger<IniciarSesionModel> logger, 
                            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                            IConsultanteMgt consultante,
                            Sesion sesion)
    {
        this.logger = logger;
        this.consultante = consultante;
        this.env = env;
        this.Persona = new PersonaModelo();
        this.sesion = sesion;
    }

    public void OnPostEntrar()
    {
        try
        {
            this.Persona.Miembros.ElementAt(0).Contrasena = this.Contrasena;
            this.sesion.Credenciales = this.consultante.IniciarSesion(this.Persona);
            this.logger.LogInformation($"Email: {this.sesion.Credenciales.Miembro.Persona.Email}");
            this.logger.LogInformation($"Contrasena: {this.sesion.Credenciales.Miembro.Contrasena}");
            this.logger.LogInformation($"Respuesta: {this.sesion.Credenciales}");
            if (this.sesion.Credenciales != null)
            {
                this.Request.HttpContext.Session.SetString("Email", this.sesion.Credenciales.Miembro.Persona.Email);
                this.Request.HttpContext.Session.SetString("Token", this.sesion.Credenciales.Token);
                this.consultante.Token = this.sesion.Credenciales.Token;
                this.Request.HttpContext.Session.SetString("Nombre", this.sesion.Credenciales.Miembro.Persona.Nombre);
                this.Response.Redirect("Menu");
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
                await this.consultante.RegistrarMiembro(Persona);
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