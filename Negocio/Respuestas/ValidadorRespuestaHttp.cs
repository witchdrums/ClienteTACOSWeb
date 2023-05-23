
namespace ClienteTACOSWeb.Negocio
{
    public class ValidadorRespuestaHttp
    {
        public static void Validar(HttpResponseMessage respuesta)
        {
            if (!respuesta.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    respuesta.Content.ReadAsAsync<Error>().Result.Mensaje
                );
            }
        }
        private class Error
        {
            public string Mensaje { get; set; }
        }
    }

}
