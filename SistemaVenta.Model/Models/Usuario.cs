using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Nombrecompleto { get; set; }

    public string? Correo { get; set; }

    public int? Idrol { get; set; }

    public string? Clave { get; set; }

    public bool? Esactivo { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public virtual Rol? IdrolNavigation { get; set; }
}
