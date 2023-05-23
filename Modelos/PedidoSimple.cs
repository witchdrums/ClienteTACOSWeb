using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClienteTACOSWeb.Modelos;

public class PedidoSimple
{
    public int Id { get; set; }

    public double? Total { get; set; } = 0;

    public int IdMiembro { get; set; }

    public int? Estado { get; set; }

    public DateTime? Fecha { get; set; }

    public PedidoSimple(PedidoModelo pedidoModelo)
    {
        this.Id = pedidoModelo.Id;
        this.Total = pedidoModelo.Total;
        this.IdMiembro = pedidoModelo.IdMiembro;
        this.Estado = pedidoModelo.Estado;
        this.Fecha = pedidoModelo.Fecha;
    }
    public PedidoSimple(){}
}