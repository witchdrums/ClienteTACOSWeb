namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using ClienteTACOSWeb.Negocio.Peticiones;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using ClienteTACOSWeb.Negocio.Respuestas;

public class ConsultanteMgr : IConsultanteMgt
{
    private PersonaModelo miembroEnSesion = new PersonaModelo();
    PedidoModelo pedido = new PedidoModelo();
    List<PedidoModelo> pedidos = new List<PedidoModelo>();
    List<ResenaModelo>? resenas;
    double total = 0;
    HttpClient cliente;
    private Sesion sesion;
    public PedidoModelo PedidoSeleccionado { set; get; }
    public ConsultanteMgr(IHttpClientFactory fcliente,
                          Sesion sesion) 
    {
        this.cliente = fcliente.CreateClient("tacos");
        this.sesion = sesion;
        //CursosAPI _cursosAPI
    }


    public void AgregarAlimentoAPedido(AlimentoModelo alimento)
    {
        if (!this.pedido.Alimentos.ContainsKey(alimento.Id))
        {
            this.pedido.Alimentos.Add(
                alimento.Id,
                alimento
            );
        }
        this.pedido.Alimentos[alimento.Id].Cantidad++;
        this.pedido.Total += alimento.Precio;
    }

    public Credenciales IniciarSesion(PeticionCredenciales credenciales)
    {
        HttpResponseMessage respuestaHttp = 
            this.cliente.PostAsJsonAsync("login", credenciales).Result;
        
        respuestaHttp.EnsureSuccessStatusCode();

        Credenciales respuesta = 
            respuestaHttp.Content
                         .ReadAsAsync<Credenciales>()
                         .Result;

        return respuesta;
    }

    public Dictionary<int, int> CancelarPedido()
    {
        Dictionary<int,int> existenciasARegresar = new Dictionary<int,int>();
        foreach (KeyValuePair<int,AlimentoModelo> registro in this.pedido.Alimentos)
        {
            existenciasARegresar.Add(registro.Key, registro.Value.Cantidad);
            registro.Value.Cantidad = 0;
        }
        this.pedido = new PedidoModelo();
        return existenciasARegresar;
    }

    public PersonaModelo ObtenerMiembroEnSesion()
    {
        return this.miembroEnSesion;
    }

    public PedidoModelo ObtenerPedido()
    {
        return this.pedido;
    }

    public List<PedidoModelo> ObtenerPedidos(DateTime desde, DateTime hasta)
    {
        cliente.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", this.sesion.Credenciales.Token);
        HttpResponseMessage respuesta = this.cliente.PostAsJsonAsync(
            "pedidos/ObtenerPedidosEntre",
            new { desde, hasta}
        ).Result;
        respuesta.EnsureSuccessStatusCode();
        this.pedidos = respuesta.Content
                        .ReadAsAsync<Respuesta<List<PedidoModelo>>>()
                        .Result
                        .Datos;
        return pedidos;
    }

    public async void ActualizarPedido(PedidoSimple pedido)
    {
        this.cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", this.sesion.Credenciales.Token);
        HttpResponseMessage respuesta= await this.cliente.PatchAsJsonAsync(
            "pedidos",
            pedido
        );
        respuesta.EnsureSuccessStatusCode();
        
    }

    public PedidoModelo SeleccionarPedido(int IdPedido)
    {
        this.PedidoSeleccionado = this.pedidos
                .FirstOrDefault(p => p.Id == IdPedido)
                ?? new PedidoModelo();
        return this.PedidoSeleccionado;
    }

    public void RegistrarMiembro(MiembroModelo miembro)
    {
        var respuestaHttp = this.cliente.PostAsJsonAsync( "miembro", miembro).Result;
        respuestaHttp.EnsureSuccessStatusCode();
        var respuesta = respuestaHttp.Content.ReadAsAsync<Respuesta<MiembroModelo>>().Result;
        if (!respuesta.OperacionExitosa)
        {
            throw new HttpRequestException($"{respuesta.Mensaje}");
        }
    }

    public void RegistrarPedido()
    {
        foreach (KeyValuePair<int,AlimentoModelo> registro in this.pedido.Alimentos)
        {
            this.pedido.Alimentospedidos.Add(
                new AlimentoPedidoModelo { 
                    IdAlimento=registro.Key, 
                    Cantidad=registro.Value.Cantidad
                });
        }
        this.pedido.IdMiembro=sesion.Credenciales.Miembro.Id;
        this.cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", this.sesion.Credenciales.Token);
        HttpResponseMessage respuesta= 
            this.cliente.PostAsJsonAsync(
                "pedidos",
                pedido
            ).Result;
        this.pedido = new PedidoModelo();
        respuesta.EnsureSuccessStatusCode();
    }

    /*public async Task ObtenerResenas2()
    {
        var response = cliente.GetAsync("Resenas").Result;
        response.EnsureSuccessStatusCode();
        this.resenas = response.Content.ReadAsAsync<List<ResenaModelo>>().Result;
    }*/

    public List<ResenaModelo> ObtenerResenas()
    {
        try {            
            var response = cliente.GetAsync("Resenas").Result;
            response.EnsureSuccessStatusCode();
            resenas = response.Content.ReadAsAsync<List<ResenaModelo>>().Result;
            return resenas;
        } catch (AggregateException) {
            return new List<ResenaModelo>();
        }
    }

    public HttpResponseMessage EliminarResena(int idResena)
    {
        var url = $"Resenas?idResena={idResena}";
        var response = this.cliente.PostAsync(url, null).Result;
        return response;
    }

}