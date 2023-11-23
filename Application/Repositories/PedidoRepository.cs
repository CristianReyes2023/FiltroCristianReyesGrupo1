using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories;
public class PedidoRepository : GenericRepository<Pedido>, IPedido
{
    private readonly JardineriaContext _context;

    public PedidoRepository(JardineriaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> GetPedidosNoEntregadosATiempo()
    {
        var results = await (from _pedido in _context.Pedidos
                            join _cliente in _context.Clientes on _pedido.IdClienteFk equals _cliente.Id
                            where _pedido.FechaEntrega > _pedido.FechaEsperada
                            select new 
                            {
                                CodigoCliente = _cliente.Id,
                                CodigoPedido = _pedido.Id,
                                FechaEsperada = _pedido.FechaEsperada,
                                FechaEntrega = _pedido.FechaEntrega

                            }).ToListAsync();
        return results;
    }
}
