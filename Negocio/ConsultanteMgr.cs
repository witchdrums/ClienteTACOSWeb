namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

public class ConsultanteMgr : IConsultanteMgt
{
    private PersonaModelo miembroEnSesion = new PersonaModelo();
    PedidoModelo pedido = new PedidoModelo();
    List<PedidoModelo> pedidos = new List<PedidoModelo>();
    double total = 0;
    public string Token { set; get; } 
    HttpClient cliente;
    private Sesion sesion;
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

    public Credenciales IniciarSesion(PersonaModelo persona)
    {
        //persona.LlenarPropiedades();
        HttpResponseMessage respuesta = 
            this.cliente.PostAsJsonAsync(
                "login", 
                new { email = persona.Email, contrasena = persona.Miembros.ElementAt(0).Contrasena}
            ).Result;
        ValidadorRespuestaHttp.Validar(respuesta);
        return respuesta.Content
                        .ReadAsAsync<Credenciales>()
                        .Result;
        
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
        HttpResponseMessage respuesta = this.cliente.PostAsJsonAsync(
            "pedidos/ObtenerPedidosEntre",
            new { desde, hasta}
       ).Result;
        respuesta.EnsureSuccessStatusCode();
        this.pedidos = respuesta.Content
                        .ReadAsAsync<List<PedidoModelo>>()
                        .Result;
        return pedidos;
    }

    public async void ActualizarPedido(PedidoSimple pedido)
    {
        this.cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", this.Token);
        HttpResponseMessage respuesta= await this.cliente.PatchAsJsonAsync(
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
        ValidadorRespuestaHttp.Validar(await this.cliente.PostAsJsonAsync(
            "miembro",
            persona
        ));

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
            new AuthenticationHeaderValue("Bearer", this.Token);
        HttpResponseMessage respuesta= this.cliente.PostAsJsonAsync(
            "pedidos",
            pedido
        ).Result;
        this.pedido = new PedidoModelo();
        respuesta.EnsureSuccessStatusCode();
    }
}