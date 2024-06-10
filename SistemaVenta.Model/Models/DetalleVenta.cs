using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Detalleventa
{
    public int Iddetalleventa { get; set; }

    public int? Idventa { get; set; }

    public int? Idproducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Producto? IdproductoNavigation { get; set; }

    public virtual Venta? IdventaNavigation { get; set; }
}
