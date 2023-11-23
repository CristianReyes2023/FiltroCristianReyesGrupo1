# Proyecto Final Cristian Leonardo Reyes Moreno

Este proyecto final consiste en el desarrollo de una página web de jardinería. La implementación en backend

## CONSULTAS 

#### Consulta 1
Devuelve un listado con el codigo de pedido,codigo cliente,fecha esperada y fecha de entrega de los pedidos que no han sido entregados a timepo

##### ENDPOINT:http://localhost:5004/Pedido/GetPedidosNoEntregadosATiempo
Primero definimos el codigo en la interfaz de pedido.
```c#
    Task<IEnumerable<Object>> GetPedidosNoEntregadosATiempo();
```
Segundo definimos el codigo para acceder a los datos en el repositorio de pedido
```c#
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
```
Finalmente definimos el endpoind de para el metodo en el controlador de pedido
```c#
    [HttpGet("GetPedidosNoEntregadosATiempo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> GetPedidosNoEntregadosATiempo()
    {
        var results = await _unitOfWork.Pedidos.GetPedidosNoEntregadosATiempo();
        return _mapper.Map<List<Object>>(results);
    }
```c#





