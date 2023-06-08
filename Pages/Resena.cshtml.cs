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
    public ResenaModel (ILogger<ResenaModel> logger,
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
    }

    public void OnPostEliminarResena(int resenaId)
    {
        /*if (Request.HttpContext.Session.GetString("Token") is null)
        {
            Response.Redirect("IniciarSesion");
        }else
        {
            INCLUIR AQUI LA LOGICA QUE ESTA DEBAJP EN LA ENTREGA FINAL
        }
        */
        HttpResponseMessage response = this._consultante.EliminarResena(resenaId);
        
        if (!response.IsSuccessStatusCode)
        {
            string jsonContent = response.Content.ReadAsStringAsync().Result;
            JObject responseObject = JObject.Parse(jsonContent);

            ErrorMessage = responseObject["mensaje"].ToString();
        }
        else
        {
            SuccessMessage = "Operación Exitosa";
        } 
    }
}