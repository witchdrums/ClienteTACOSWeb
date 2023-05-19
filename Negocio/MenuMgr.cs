namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;


public class MenuMgr : IMenuMgt
{
    List<AlimentoModelo> menu = null;
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
        try {            
            var response = _cliente.GetAsync("menu/alimentos").Result;
            response.EnsureSuccessStatusCode();
            menu = response.Content.ReadAsAsync<List<AlimentoModelo>>().Result;
            return menu;
        } catch (AggregateException e) {
            Console.WriteLine(e.Message);
            return new List<AlimentoModelo>();
        }
    }
    
    public AlimentoModelo ObtenerAlimento(int idAlimento)
    {
        AlimentoModelo alimentoEncontrado = this.menu.FirstOrDefault(a => a.Id == idAlimento);
        if (alimentoEncontrado is null)
        {
            alimentoEncontrado = new AlimentoModelo();
        }
        return alimentoEncontrado; 
    }
}
