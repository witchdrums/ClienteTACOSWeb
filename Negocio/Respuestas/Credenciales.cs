using ClienteTACOSWeb.Modelos;

namespace ClienteTACOSWeb.Negocio;
public class Credenciales : RespuestaBase
{
    public MiembroModelo? Miembro { get; set; } = new MiembroModelo();
    public string Staff { get; set; }
    public string Token { get; set; }
    public string Expira { get; set; }

}