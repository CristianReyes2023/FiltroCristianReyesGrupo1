using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories;
public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly JardineriaContext _context;

    public ClienteRepository(JardineriaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> GetClienteNoPagosYRepresentanteCiudad()
    {
        var results = await (from _cliente in _context.Clientes
                            join _pagos in _context.Pagos on _cliente.Id equals _pagos.IdClienteFk
                            join _empleado in _context.Empleados on _cliente.IdEmpleadoRepresentanteVentasFk equals _empleado.Id
                            join _oficina in _context.Oficinas on _empleado.IdOficinaFk equals _oficina.Id
                            where _context.Pagos.Any(x=>x.IdClienteFk == _cliente.Id)
                            select new 
                            {
                                IdCliente = _cliente.Id,
                                NombreCliente = _cliente.NombreCliente,
                                NombreRepresentates = _empleado.Nombre,
                                CiudadOficina = _oficina.Ciudad

                            }).ToListAsync();
        return results;
    }
}
