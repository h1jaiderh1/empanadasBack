using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Venta
{
    public int Idventa { get; set; }

    public string? Numerodocumento { get; set; }

    public string? Tipopago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();
}
