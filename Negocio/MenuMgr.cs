namespace ClienteTACOSWeb.Negocio;
using ClienteTACOSWeb.Modelos;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;


public class MenuMgr : IMenuMgt
{
    ObservableCollection<AlimentoModelo>? menu;
    PedidoModelo pedido = new PedidoModelo();
    double total = 0;
    HttpClient _cliente;
    public MenuMgr(IHttpClientFactory fcliente) 
    {
        _cliente = fcliente.CreateClient("tacos");
        //CursosAPI _cursosAPI
    }
    public ObservableCollection<AlimentoModelo> ObtenerMenu()
    {
        try {            
            var response = _cliente.GetAsync("menu/alimentos").Result;
            response.EnsureSuccessStatusCode();
            menu = response.Content.ReadAsAsync<ObservableCollection<AlimentoModelo>>().Result;
            return menu;
        } catch (AggregateException) {
            return new ObservableCollection<AlimentoModelo>();
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
}
