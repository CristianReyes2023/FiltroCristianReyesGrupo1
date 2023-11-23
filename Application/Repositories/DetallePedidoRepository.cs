using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories;
public class DetallePedidoRepository : GenericRepositoryCompuesto<DetallePedido>, IDetallePedido
{
    private readonly JardineriaContext _context;

    public DetallePedidoRepository(JardineriaContext context) : base(context)
    {
        _context = context;
    }
       public async Task<IEnumerable<Object>> Get20ProductosMásVendidos()
    {
        var result = await (from _detallepedidos in _context.DetallePedidos
                            group _detallepedidos  by _detallepedidos.IdProductoFk into _ptotalven
                            select new
                            {
                                IdProducto = _ptotalven.Key,
                                TotalVendidos = _ptotalven.Sum(x=>x.Cantidad)

                            }).OrderByDescending(x=>x.TotalVendidos).Take(20).ToListAsync();
        return result;
    }
}
