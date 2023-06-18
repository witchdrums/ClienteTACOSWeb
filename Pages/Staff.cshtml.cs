using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using ClienteTACOSWeb.Negocio.Peticiones;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace ClienteTACOSWeb.Pages;

public class StaffModel : PageModel
{
    private readonly ILogger<IniciarSesionModel> logger;
    public IConsultanteMgt consultante { get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment env;

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "No puede estar vacío")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; }

    [BindProperty(SupportsGet = true)]
    public StaffModelo Staff { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PuestoSeleccionadoId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int TurnoSeleccionadoId { get; set; }
    public List<PuestoModelo> Puestos { get; set; }
    public List<SelectListItem> PuestosSelectList { get; set; }

    public List<TurnoModelo> Turnos { get; set; }
    public List<SelectListItem> TurnosSelectList { get; set; }
    public string ErrorMessage;
    public string SuccessMessage;

    public StaffModel(ILogger<IniciarSesionModel> logger,
                            Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                            IConsultanteMgt _consultante,
                            Sesion sesion)
    {
        this.logger = logger;
        this.consultante = _consultante;
        this.env = env;
        this.Staff = new StaffModelo();
    }

    public void OnGet()
    {
        RecuperarPuestos();
        RecuperarTurnos();
    }

    private void RecuperarPuestos()
    {
        HttpResponseMessage respuesta = this.consultante.ObtenerPuestos();
        if (!respuesta.IsSuccessStatusCode)
        {
            if (((int)respuesta.StatusCode) != StatusCodes.Status401Unauthorized)
            {
                string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                JObject responseObject = JObject.Parse(jsonContent);
                ErrorMessage = responseObject["mensaje"].ToString();
                OnGet();
            }
            else
            {
                Response.Redirect("IniciarSesion");
            }
        }
        else
        {
            Puestos = respuesta.Content.ReadAsAsync<List<PuestoModelo>>().Result;
            PuestosSelectList = Puestos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Cargo,
                Selected = p.Id == PuestoSeleccionadoId
            }).ToList();
        }
    }

    private void RecuperarTurnos()
    {
        HttpResponseMessage respuesta = this.consultante.ObtenerTurnos();
        if (!respuesta.IsSuccessStatusCode)
        {
            if (((int)respuesta.StatusCode) != StatusCodes.Status401Unauthorized)
            {
                string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                JObject responseObject = JObject.Parse(jsonContent);
                ErrorMessage = responseObject["mensaje"].ToString();
                OnGet();
            }
            else
            {
                Response.Redirect("IniciarSesion");
            }
        }
        else
        {
            Turnos = respuesta.Content.ReadAsAsync<List<TurnoModelo>>().Result;
            TurnosSelectList = Turnos.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Tipo,
                Selected = t.Id == TurnoSeleccionadoId
            }).ToList();
        }
    }

    public void OnPostRegistrarEstaff()
    {
        ErrorMessage = "";
        var errors = ModelState.Values.SelectMany(v => v.Errors);

        if (TryValidateModel(this.Staff, nameof(this.Staff)))
        {

            RegistrarEstaff();
        }
        else
        {
            ErrorMessage = "Campos invalidos, verifica los datos que proporcionaste por favor";
            OnGet();
        }
    }

    private void RegistrarEstaff()
    {
        this.Staff.Contrasena = this.Contrasena;
        this.Staff.IdPuesto = this.PuestoSeleccionadoId;
        this.Staff.IdTurno = this.TurnoSeleccionadoId;
        HttpResponseMessage respuesta = this.consultante.RegistrarStaff(this.Staff);
        if (!respuesta.IsSuccessStatusCode)
        {
            if (((int)respuesta.StatusCode) != StatusCodes.Status401Unauthorized)
            {
                string jsonContent = respuesta.Content.ReadAsStringAsync().Result;
                JObject responseObject = JObject.Parse(jsonContent);
                ErrorMessage = responseObject["mensaje"].ToString();
                OnGet();
            }
            else
            {
                Response.Redirect("IniciarSesion");
            }
        }
        else
        {
            SuccessMessage = "Nuevo miembro ingresado al Staff. Recuerda todos somos familia ;D";
            this.Staff = null;
            ModelState.Clear();
            OnGet();
            //RedirectToAction(nameof(StaffModel));
        }
    }
}