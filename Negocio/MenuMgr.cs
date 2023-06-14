namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using Grpc.Core;
using IMG;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using ClienteTACOSWeb.Negocio.Respuestas;


public class MenuMgr : IMenuMgt
{
    List<AlimentoModelo>? menu;
    PedidoModelo pedido = new PedidoModelo();
    double total = 0;
    HttpClient _cliente;
    public MenuMgr(IHttpClientFactory fcliente) 
    {
        _cliente = fcliente.CreateClient("tacos");
        //CursosAPI _cursosAPI
    }
    public List<AlimentoModelo> ObtenerMenu()
    {
        return this.menu;
    }
    public async Task CargarMenu()
    {
        var respuestaHttp = _cliente.GetAsync("menu/ObtenerAlimentosConImagenes").Result;
        respuestaHttp.EnsureSuccessStatusCode();
        if (respuestaHttp.IsSuccessStatusCode)
        {
            menu = respuestaHttp.Content
                   .ReadAsAsync<Respuesta<List<AlimentoModelo>>>()
                   .Result
                   .Datos;
        }
        else
        {
            this.menu = new List<AlimentoModelo>();

            var respuesta = respuestaHttp.Content
                                         .ReadAsAsync<Respuesta<List<AlimentoModelo>>>()
                                         .Result;
            throw new HttpRequestException($"{(int)respuesta.Codigo}: { respuesta.Mensaje}");
        }
    }
    
    public AlimentoModelo ObtenerAlimento(int idAlimento)
    {
        AlimentoModelo? alimentoEncontrado = this.menu?.FirstOrDefault(a => a.Id == idAlimento);
        if (alimentoEncontrado is null)
        {
            alimentoEncontrado = new AlimentoModelo();
        }
        return alimentoEncontrado; 
    }

    public void ActualizarExistenciaAlimentos(Dictionary<int, int> idAlimentos_Cantidades)
    {
        HttpResponseMessage respuesta =
            this._cliente.PatchAsJsonAsync(
                "menu",
                idAlimentos_Cantidades
            ).Result;
        respuesta.EnsureSuccessStatusCode();
        foreach (KeyValuePair<int,int> registro in idAlimentos_Cantidades)
        {
            this.menu
                .FirstOrDefault(a => a.Id == registro.Key)
                .Existencia += registro.Value;
        }
    }
}
