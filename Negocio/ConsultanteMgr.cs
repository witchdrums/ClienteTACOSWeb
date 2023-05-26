namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;


public class ConsultanteMgr : IConsultanteMgt
{
    private PersonaModelo miembroEnSesion = new PersonaModelo();
    PedidoModelo pedido = new PedidoModelo();
    List<PedidoModelo> pedidos = new List<PedidoModelo>();
    double total = 0;
    HttpClient _cliente;
    public ConsultanteMgr(IHttpClientFactory fcliente) 
    {
        _cliente = fcliente.CreateClient("tacos");
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

    public RespuestaIniciarSesion IniciarSesion(PersonaModelo persona)
    {
        //persona.LlenarPropiedades();
        HttpResponseMessage respuesta = 
            this._cliente.PostAsJsonAsync(
                "miembros", 
                new { email = persona.Email, contrasena = persona.Miembros.ElementAt(0).Contrasena}
            ).Result;
        ValidadorRespuestaHttp.Validar(respuesta);
        return respuesta.Content
                        .ReadAsAsync<RespuestaIniciarSesion>()
                        .Result;
        
    }

    public PersonaModelo ObtenerMiembroEnSesion()
    {
        return this.miembroEnSesion;
    }

    public PedidoModelo ObtenerPedido()
    {
        return this.pedido;
    }

    public List<PedidoModelo> ObtenerPedidos()
    {
        HttpResponseMessage respuesta = this._cliente.GetAsync("pedidos").Result;
        respuesta.EnsureSuccessStatusCode();
        this.pedidos = respuesta.Content
                        .ReadAsAsync<List<PedidoModelo>>()
                        .Result;
        return pedidos;
    }

    public async void ActualizarPedido(PedidoSimple pedido)
    {

        HttpResponseMessage respuesta= await this._cliente.PatchAsJsonAsync(
            "pedidos",
            pedido
        );
        respuesta.EnsureSuccessStatusCode();
        
    }

    public PedidoModelo ObtenerPedidoLocal(int IdPedido)
    {
        this.pedido = 
            this.pedidos
                .FirstOrDefault(p => p.Id == IdPedido) 
                ?? new PedidoModelo();
        return this.pedido;
    }

    public async Task RegistrarMiembro(PersonaModelo persona)
    {
        ValidadorRespuestaHttp.Validar(await this._cliente.PostAsJsonAsync(
            "persona",
            persona
        ));

    }
}