using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;

namespace ClienteTACOSWeb.Pages;

public class _LayoutModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IConsultanteMgt _consultante {get; set; }
    private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    public MiembroModelo Miembro { get; set; }

    [BindProperty(SupportsGet =true)]
    public bool esLogin { get; set; }

    public _LayoutModel(ILogger<IndexModel> logger, 
                       Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                       IConsultanteMgt consultante)
    {
        _logger = logger;
        _consultante = consultante;
        _env = env;
        
    }

    public void OnPostEntrar()
    {
        Console.WriteLine($"{Miembro.Persona.Email} y {Miembro.Contrasena}");
    }

}