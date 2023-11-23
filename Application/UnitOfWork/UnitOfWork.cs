using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private ICliente _clientes;
    private IDetallePedido _detallePedidos;
    private IEmpleado _empleados;
    private IGamaProducto _gamaProductos;
    private IOficina _oficinas;
    private IPago _pagos;
    private IPedido _pedidos;
    private IProducto _productos;

    private readonly JardineriaContext _context;

    public UnitOfWork(JardineriaContext context)
    {
        _context = context;
    }

    public ICliente Clientes // 2611
    {
        get
        {
            if (_clientes == null)
            {
                _clientes = new ClienteRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _clientes;
        }
    }
        public IDetallePedido DetallePedidos // 2611
    {
        get
        {
            if (_detallePedidos == null)
            {
                _detallePedidos = new DetallePedidoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _detallePedidos;
        }
    }
        public IEmpleado Empleados // 2611
    {
        get
        {
            if (_empleados == null)
            {
                _empleados = new EmpleadoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _empleados;
        }
    }
        public IGamaProducto GamaProductos // 2611
    {
        get
        {
            if (_gamaProductos == null)
            {
                _gamaProductos = new GamaProductoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _gamaProductos;
        }
    }
        public IOficina Oficinas // 2611
    {
        get
        {
            if (_oficinas == null)
            {
                _oficinas = new OficinaRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _oficinas;
        }
    }
        public IPago Pagos // 2611
    {
        get
        {
            if (_pagos == null)
            {
                _pagos = new PagoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _pagos;
        }
    }
        public IPedido Pedidos // 2611
    {
        get
        {
            if (_pedidos == null)
            {
                _pedidos = new PedidoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _pedidos;
        }
    }
        public IProducto Productos // 2611
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepository(_context); // Remember putting the base in the repository of this entity
            }
            return _productos;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
