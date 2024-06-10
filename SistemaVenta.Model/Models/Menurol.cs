using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Menurol
{
    public int Idmenurol { get; set; }

    public int? Idmenu { get; set; }

    public int? Idrol { get; set; }

    public virtual Menu? IdmenuNavigation { get; set; }

    public virtual Rol? IdrolNavigation { get; set; }
}
