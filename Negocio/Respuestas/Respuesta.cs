using System.Net;

namespace ClienteTACOSWeb.Negocio.Respuestas;

public class Respuesta<T>
{
    public T Datos { set; get; }
    public HttpStatusCode Codigo { set; get; }
    public string Mensaje { set; get; }
    public bool OperacionExitosa => ((int)this.Codigo) > 199 && ((int)this.Codigo) < 300;
}