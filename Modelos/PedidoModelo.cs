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

    public virtual MiembroModelo? Miembro { get; set; } = null!;

    public string NombreMiembro => $"{this.Miembro.Persona.Nombre} "        
                                    + $"{this.Miembro.Persona.ApellidoPaterno} " 
                                    + $"{this.Miembro.Persona.ApellidoMaterno}";
    public string EstadoStr => ((Estados)Estado).ToString();
    //public virtual ICollection<Alimentospedido> Alimentospedidos { get; set; } = new List<Alimentospedido>();

    //[JsonIgnore]
    //public virtual Miembro? Miembro { get; set; } = null!;

    public PedidoModelo(){}
    public PedidoModelo(PedidoSimple pedidoModelo)
    {
        this.Id = pedidoModelo.Id;
        this.Total = pedidoModelo.Total;
        this.IdMiembro = pedidoModelo.IdMiembro;
        this.Estado = pedidoModelo.Estado;
        this.Fecha = pedidoModelo.Fecha;
    }
}