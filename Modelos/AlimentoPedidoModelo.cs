using System.Text.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClienteTACOSWeb.Modelos;

public partial class AlimentoPedidoModelo
{
    public int Id { get; set; }

    public int? Cantidad { get; set; }

    public int IdAlimento { get; set; }

    public int IdPedido { get; set; }

    public virtual AlimentoModelo? Alimento { get; set; } = null!;

    [JsonIgnore]
    public string Subtotal =>
        string.Format("${0:#.00}", (this.Cantidad * this.Alimento.Precio));
}

