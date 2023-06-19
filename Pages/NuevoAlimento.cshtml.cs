using ClienteTACOSWeb.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClienteTACOSWeb.Pages
{


    public class NuevoAlimentoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public AlimentoModelo NuevoAlimento { get; set; } = new AlimentoModelo();

        [BindProperty(SupportsGet = true)]
        public IFormFile NuevaImagen { set; get; }


        private IWebHostEnvironment _environment;
        public NuevoAlimentoModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
        }
        public void OnPostRegistrarAlimento(IFormFile imagen) 
        {
            
            var file = Path.Combine(_environment.ContentRootPath, "uploads", NuevaImagen.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                this.NuevoAlimento.Imagen = new Imagen();
                this.NuevoAlimento.Imagen.ImagenBytes = memoryStream.ToArray();
            }
        }
    }
}
