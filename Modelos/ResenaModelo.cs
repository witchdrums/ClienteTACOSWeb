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

public class ResenaModelo : INotifyPropertyChanged
{
    //Columnas
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public byte[]? Imagen { get; set; }
    public DateTime Fecha { get; set; }
    public int IdMiembro { get; set; }
    public MiembroModelo Miembro { get; set; }

    //Otros
    public string ImagenConvertida
    {
        get
        {
            if (Imagen != null) {


                return String.Format("data:image/png;base64,{0}",
                       Convert.ToBase64String(Imagen));
            }
            return null;
        }
    }
    
    //INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
}