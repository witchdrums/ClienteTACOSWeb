using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClienteTACOSWeb.Modelos;

public partial class PedidoModelo
{
    public int Id { get; set; }

    public double? Total { get; set; } = 0;
    public string TotalStr =>
        string.Format("${0:#.00}", Total);

    public int IdMiembro { get; set; }

    public int? Estado { get; set; } = 1;

    public DateTime? Fecha { get; set; } = DateTime.Now;

    [JsonIgnore]
    public Dictionary<int, AlimentoModelo> Alimentos { get; set; } =
        new Dictionary<int, AlimentoModelo>();

    public virtual MiembroModelo? Miembro { get; set; } = null!;

    [JsonIgnore]
    public string NombreMiembro => $"{this.Miembro.Persona.Nombre} "        
                                    + $"{this.Miembro.Persona.ApellidoPaterno} " 
                                    + $"{this.Miembro.Persona.ApellidoMaterno}";
    [JsonIgnore]
    public string EstadoStr => ((Estados)Estado).ToString();
    public virtual ICollection<AlimentoPedidoModelo> Alimentospedidos { get; set; } = new List<AlimentoPedidoModelo>();

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