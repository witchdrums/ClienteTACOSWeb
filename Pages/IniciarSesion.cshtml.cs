using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using ClienteTACOSWeb.Negocio.Peticiones;

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
    public MiembroModelo Miembro { set; get; }

    [BindProperty(SupportsGet = true)]
    public PeticionCredenciales PeticionCredenciales { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool Error { get; set; }

    [BindProperty(SupportsGet = true)]
    public string MensajeError { get; set; } = "Sin mensaje";

    [BindProperty(SupportsGet = true)]
    public bool EsStaff { get; set; }

    private Sesion sesion;
    public IniciarSesionModel(ILogger<IniciarSesionModel> logger, 
                            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                            IConsultanteMgt consultante,
                            Sesion sesion)
    {
        this.logger = logger;
        this.consultante = consultante;
        this.env = env;
        this.Miembro = new MiembroModelo();
        this.sesion = sesion;
    }

    public void OnPostEntrar()
    {
        try
        {
            this.Miembro.Contrasena = this.Contrasena;
            this.sesion.Credenciales = this.consultante.IniciarSesion(this.PeticionCredenciales);
            PersonaModelo personaObtenida = new PersonaModelo();
            if (this.PeticionCredenciales.EsStaff)
            {
                personaObtenida = this.sesion.Credenciales.Staff.Persona;
                this.Request.HttpContext.Session.SetString("Puesto", this.sesion.Credenciales.Staff.IdPuesto.ToString());
            }
            else
            {
                personaObtenida = this.sesion.Credenciales.Miembro.Persona;
                this.Request.HttpContext.Session.SetString("Puesto", "0");
            }
            this.logger.LogInformation($"Email: {personaObtenida.Email}");
            if (this.sesion.Credenciales != null)
            {
                this.Request.HttpContext.Session.SetString("Email", personaObtenida.Email);
                this.Request.HttpContext.Session.SetString("Token", this.sesion.Credenciales.Token);
                this.Request.HttpContext.Session.SetString("Nombre", personaObtenida.Nombre);
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


    public void OnPostUnirse()
    {
        ModelState.Remove("MensajeError");

        var errors = ModelState.Values.SelectMany(v => v.Errors);

        if (TryValidateModel(this.Miembro, nameof(this.Miembro)))
        {
            try
            {
                this.Miembro.Contrasena = this.Contrasena;
                this.consultante.RegistrarMiembro(this.Miembro);
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