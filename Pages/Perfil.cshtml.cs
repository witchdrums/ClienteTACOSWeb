using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClienteTACOSWeb.Pages
{
    public class PerfilModel : PageModel
    {

        private readonly ILogger<IniciarSesionModel> logger;
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment env;
        private Sesion sesion;
        public PerfilModel(ILogger<IniciarSesionModel> logger,
                                Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                                Sesion sesion)
        {
            this.logger = logger;
            this.env = env;
            this.sesion = sesion;
        }
        public void OnGet()
        {
        }

        public void OnPostCerrarSesion()
        {
            this.Request.HttpContext.Session.SetString("Email", "");
            this.Request.HttpContext.Session.SetString("Token", "");
            this.Request.HttpContext.Session.SetString("Nombre", "");
            this.Request.HttpContext.Session.SetString("Puesto", "-1");
            this.sesion.Credenciales = new Credenciales();
            this.Response.Redirect("Menu");
        }
    }
}
