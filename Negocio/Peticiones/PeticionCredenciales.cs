namespace ClienteTACOSWeb.Negocio.Peticiones;
public class PeticionCredenciales
{
    public string? Email { get; set; } = null;
    public string? Contrasena { get; set; } = null;
    public bool EsStaff { get; set; } = false;
}