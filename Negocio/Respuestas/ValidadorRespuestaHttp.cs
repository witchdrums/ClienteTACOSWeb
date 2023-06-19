
using ClienteTACOSWeb.Negocio.Respuestas;

namespace ClienteTACOSWeb.Negocio
{
    public class ValidadorRespuestaHttp
    {
        public static void Validar(HttpResponseMessage respuesta)
        {
            if (!respuesta.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    respuesta.Content.ReadAsAsync<Respuesta<object>>().Result.Mensaje
                );
            }
        }
        private class Error
        {
            public string Mensaje { get; set; }
        }
    }

}
