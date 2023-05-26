namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using Grpc.Core;
using IMG;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;


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
        var response = _cliente.GetAsync("menu").Result;
        response.EnsureSuccessStatusCode();

        menu = response.Content.ReadAsAsync<List<AlimentoModelo>>().Result;

        
        HashSet<int> idImagenes = new HashSet<int>();
        int numAlimentos = menu.Count();
        for (int i = 0; i < numAlimentos; i++)
        {
            idImagenes.Add(menu.ElementAt(i).IdImagen);
        }

        List<Modelos.Imagen> imagenes = this.ObtenerImagenes(idImagenes).Result;

        int numImagenes = imagenes.Count();
        for (int i = 0; i < numAlimentos; i++)
        {
            for (int j = 0; j < numImagenes; j++)
            {
                if (menu.ElementAt(i).IdImagen
                    == imagenes.ElementAt(j).Id)
                {
                    menu.ElementAt(i).Imagen = imagenes.ElementAt(j);
                }
            }
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

    public async Task<List<Modelos.Imagen>> ObtenerImagenes(HashSet<int> idImagenes)
    {
        Channel channel = 
            new Channel(
                "localhost", 
                7252,
                ChannelCredentials.Insecure
            );
        await channel.ConnectAsync().ContinueWith((task) =>
        {
            if (task.Status == TaskStatus.RanToCompletion)
                Console.WriteLine("Cliente conectado satisfactoriamente");
        });

        ImagenesRequest peticion = new ImagenesRequest();
        peticion.Id.AddRange(idImagenes);
        var cliente = new ImagenesService.ImagenesServiceClient(channel);
        var respuesta = cliente.ObtenerImagenes(peticion);
        List<Modelos.Imagen> imagenesObtenidas = new List<Modelos.Imagen>();
        foreach (IMG.Imagen imagen in respuesta.Imagen)
        {
            //Console.WriteLine($"{imagen.Id}, {imagen.Nombre}, {imagen.Imagen_.ToByteArray().Length}");
            imagenesObtenidas.Add(new Modelos.Imagen{
                Id = imagen.Id,
                Nombre = imagen.Nombre,
                ImagenBytes = imagen.Imagen_.ToByteArray(),
            });
        }

        channel.ShutdownAsync().Wait();
        return imagenesObtenidas;
    }

    public void ActualizarExistenciaAlimentos(Dictionary<int, int> idAlimentos_Cantidades)
    {
        HttpResponseMessage respuesta =
            this._cliente.PatchAsJsonAsync(
                "menu/alimentos",
                idAlimentos_Cantidades
            ).Result;
        ValidadorRespuestaHttp.Validar(respuesta);

    }
}
