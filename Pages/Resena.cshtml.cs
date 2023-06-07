using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;

namespace ClienteTACOSWeb.Pages;

public class ResenaModel : PageModel
{

    private readonly ILogger<ResenaModel> _logger;
    public IConsultanteMgt _consultante { get; set; }

    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    public ResenaModel (ILogger<ResenaModel> logger,
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
    }

    /*public async void OnGet()
    {
        await this._consultante.ObtenerResenas();
    }*/

}