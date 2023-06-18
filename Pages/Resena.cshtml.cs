using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClienteTACOSWeb.Pages;

public class ResenaModel : PageModel
{
    private readonly ILogger<ResenaModel> _logger;
    public IConsultanteMgt _consultante { get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;
    public string ErrorMessage;
    public string SuccessMessage;
    public Sesion _sesion;

    public ResenaModel (ILogger<ResenaModel> logger,
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante, Sesion sesion)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
        this._sesion = sesion;
    }

    public void OnPostEliminarResena(int resenaId)
    {
            HttpResponseMessage response = this._consultante.EliminarResena(resenaId);
        
            if (!response.IsSuccessStatusCode)
            {
                if(((int)response.StatusCode) != StatusCodes.Status401Unauthorized){
                    string jsonContent = response.Content.ReadAsStringAsync().Result;
                    JObject responseObject = JObject.Parse(jsonContent);
                    ErrorMessage = responseObject["mensaje"].ToString();
                }
                else
                {
                    Response.Redirect("IniciarSesion");
                }
            }
            else
            {
                SuccessMessage = "Reseña borrada con éxito";
            } 
    }
        
}