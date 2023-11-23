using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class DetallePedidoDto
{
    public int IdPedidoFk { get; set; }

    public string IdProductoFk { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }
}
