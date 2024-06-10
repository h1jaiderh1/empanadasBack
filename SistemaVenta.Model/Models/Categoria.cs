using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Categoria
{
    public int Idcategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? Esactivo { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
