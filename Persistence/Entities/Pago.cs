using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class Pago
{
    public int IdClienteFk { get; set; }

    public string FormaPago { get; set; } = null!;

    public string Id { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente IdClienteFkNavigation { get; set; } = null!;
}
