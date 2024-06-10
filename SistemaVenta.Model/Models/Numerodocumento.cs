using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Numerodocumento
{
    public int Idnumerodocumento { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? Fecharegistro { get; set; }
}
