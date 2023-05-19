namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using System.Net.Http;


public class ConsultanteMgr : IConsultanteMgt
{
    private PersonaModelo miembroEnSesion = new PersonaModelo();
    PedidoModelo pedido = new PedidoModelo();
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

    public void IniciarSesion(PersonaModelo persona)
    {
        HttpResponseMessage respuesta = 
            this._cliente.PostAsJsonAsync(
                "miembros", 
                persona
            ).Result;
        respuesta.EnsureSuccessStatusCode();
        this.miembroEnSesion = respuesta.Content
                                        .ReadAsAsync<PersonaModelo>()
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


}