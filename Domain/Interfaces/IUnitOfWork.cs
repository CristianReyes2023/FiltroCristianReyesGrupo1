using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IUnitOfWork
{
    ICliente Clientes { get; }
    IDetallePedido DetaIDetallePedidos { get; }
    IEmpleado EmIEmpleados { get; }
    IGamaProducto GamaIGamaProductos { get; }
    IOficina Oficinas { get; }
    IPago Pagos {get;}
    IPedido Pedidos {get;}
    IProducto ProdIProductos {get;}

    Task<int> SaveAsync();

}
