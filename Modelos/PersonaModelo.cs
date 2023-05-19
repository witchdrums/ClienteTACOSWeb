namespace ClienteTACOSWeb.Modelos
{
    public class PersonaModelo
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "nombre";

        public string ApellidoPaterno { get; set; } = "apellidoPaterno";

        public string ApellidoMaterno { get; set; } = "apellidoMaterno";

        public string Direccion { get; set; } = "direccion";

        public string Email { get; set; } = "email";

        public string Telefono { get; set; } = "telefono";

        public PersonaModelo() 
        {
            this.Miembros = new List<MiembroModelo>();
        } 
        public void LlenarPropiedades() 
        {
            Nombre = "Nombre";
            ApellidoPaterno = "ApellidoPaterno";
            ApellidoMaterno = "ApellidoMaterno";
            Direccion = "Direccion";
            Email = "Email";
            Telefono = "Telefono";
            this.Miembros = new List<MiembroModelo>();
            this.Miembros.Add(new MiembroModelo());
            Miembros.ElementAt(0).Contrasena = "Contrasena";
            Miembros.ElementAt(0).CodigoConfirmacion = 0;
        } 
        public List<MiembroModelo> Miembros { get; set; } =
            new List<MiembroModelo>(){new MiembroModelo()};

    }
}
