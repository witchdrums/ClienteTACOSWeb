using ClienteTACOSWeb.Modelos;

namespace ClienteTACOSWeb.Negocio;
public class RespuestaIniciarSesion : RespuestaBase
{
    public PersonaModelo? Persona { get; set; } = new PersonaModelo();
}