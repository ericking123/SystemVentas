using System;
using System.Collections.Generic;

namespace SysVenta.Entity;

public partial class NumeroCorrelativo
{
    public int IdNumeroCorrelativo { get; set; }

    public int? UltimoNumero { get; set; }

    public int? CantidadDigitos { get; set; }

    public string? Gestion { get; set; }

    public DateTime? FechaActualizada { get; set; }
}
