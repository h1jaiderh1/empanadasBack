using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string? Nombre { get; set; }

    public int? Idcategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? Esactivo { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Categoria? IdcategoriaNavigation { get; set; }
}
