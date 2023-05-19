using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteTACOSWeb.Modelos;

public partial class PedidoModelo
{
    public int Id { get; set; }

    public double? Total { get; set; } = 0;
    public string TotalStr =>
        string.Format("${0:#.00}", Total);
    
    public int IdMiembro { get; set; }

    public int? Estado { get; set; }

    public DateTime? Fecha { get; set; }

    public Dictionary<int, AlimentoModelo> Alimentos { get; set; } = 
        new Dictionary<int,AlimentoModelo>();


    //public virtual ICollection<Alimentospedido> Alimentospedidos { get; set; } = new List<Alimentospedido>();

    //[JsonIgnore]
    //public virtual Miembro? Miembro { get; set; } = null!;
}