using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClienteTACOSWeb.Modelos
{
    public class PersonaModelo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [DisplayName("Nombre")]
        [RegularExpression(@"^[a-zA-Z����������''-'\s]{1,40}$",
         ErrorMessage = "S�lo puede contener letras.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [RegularExpression(@"^[a-zA-Z����������''-'\s]{1,40}$",
         ErrorMessage = "S�lo puede contener letras.")]
        [DisplayName("Apellido paterno")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [RegularExpression(@"^[a-zA-Z����������''-'\s]{1,40}$",
         ErrorMessage = "S�lo puede contener letras.")]
        [DisplayName("Apellido materno")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [DisplayName("Direcci�n")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [DisplayName("Correo electr�nico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No puede estar vac�o")]
        [StringLength(200, ErrorMessage = "No puede exceder los 200 caracteres.")]
        [RegularExpression(@"^(\d{3}[- ]?){2}\d{4}$",
         ErrorMessage = "S�lo puede contener letras.")]
        [DisplayName("Telefono")]
        public string Telefono { get; set; }

        public PersonaModelo() 
        {
            this.Miembros = new List<MiembroModelo> { new MiembroModelo() };
        } 

        public string NombreCompleto => $"{this.Nombre} "        
                                        + $"{this.ApellidoPaterno} " 
                                        + $"{this.ApellidoMaterno}";
        public void LlenarPropiedades() 
        {
            Nombre = "Nombre";
            ApellidoPaterno = "ApellidoPaterno";
            ApellidoMaterno = "ApellidoMaterno";
            Direccion = "Direccion";
            Telefono = "Telefono";
            this.Miembros = new List<MiembroModelo>();
            this.Miembros.Add(new MiembroModelo());
            Miembros.ElementAt(0).CodigoConfirmacion = 0;
        } 
        public List<MiembroModelo> Miembros { get; set; } =
            new List<MiembroModelo>(){new MiembroModelo()};

    }
}
