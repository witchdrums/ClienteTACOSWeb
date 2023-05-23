using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace ClienteTACOSWeb.Modelos;

public class AlimentoModelo : INotifyPropertyChanged
{
    //Columnas
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre no puede estar vacío.")]
    [StringLength(100)]
    [DisplayName("Nombre")]
    public string? Nombre { set; get; }

    [StringLength(100)]
    [DisplayName("Descripción")]
    public string? Descripcion { set; get; }

    private int existencia;
    [DisplayName("Existencia")]
    public int Existencia
    {
        set
        {
            this.existencia = value;
            OnPropertyChanged();
        }
        get { return this.existencia; }
    }

    public int IdImagen { get; set; }
    public Imagen Imagen { set; get; }
    public double Precio { set; get; }
    public int IdCategoria { get; set; }

    public List<object>? Alimentospedidos { get; set; } 

    public object? Categoria { get; set; }   

    public bool Agotado => this.Existencia<1;
    public string PrecioStr =>
        string.Format("${0:#.00}", Precio);

    private int cantidad;
    public int Cantidad
    {
        set
        {
            this.cantidad = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Subtotal));
        }
        get { return this.cantidad; }
    }
    public string Subtotal => 
        string.Format("${0:#.00}", (this.Cantidad * this.Precio));
    

    //INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}

