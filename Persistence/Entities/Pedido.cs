﻿using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class Pedido
{
    public int Id { get; set; }

    public DateOnly FechaPedido { get; set; }

    public DateOnly FechaEsperada { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public int IdClienteFk { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Cliente IdClienteFkNavigation { get; set; } = null!;
}