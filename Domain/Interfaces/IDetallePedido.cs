using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
public interface IDetallePedido : IGenericRepositoryCompuesto<DetallePedido>
{
    Task<IEnumerable<Object>> Get20ProductosMásVendidos();
}
