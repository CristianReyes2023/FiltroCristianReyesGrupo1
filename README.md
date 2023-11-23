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

#### Consulta 2
Develve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficna a la que pertenece el representante.

##### ENDPOINT:http://localhost:5004/Cliente/GetClienteNoPagosYRepresentanteCiudad
Primero definimos el codigo en la interfaz de cliente.
```c#
    Task<IEnumerable<Object>> GetClienteNoPagosYRepresentanteCiudad();
```
Segundo definimos el codigo para acceder a los datos en el repositorio de pedido
```c#
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
```
Finalmente definimos el endpoind de para el metodo en el controlador de cliente
```c#
    [HttpGet("GetClienteNoPagosYRepresentanteCiudad")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> GetClienteNoPagosYRepresentanteCiudad()
    {
        var results = await _unitOfWork.Clientes.GetClienteNoPagosYRepresentanteCiudad();
        return _mapper.Map<List<Object>>(results);
    }
```c#

#### Consulta 3
Devuelve las oficinas donde no trabajen ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la fama frutales

##### ENDPOINT:http://localhost:5004/Oficina/GetOficinaNotrabajanProductFrutales
Primero definimos el codigo en la interfaz de oficina
```c#
    Task<IEnumerable<Object>> GetClienteNoPagosYRepresentanteCiudad();
```
Segundo definimos el codigo para acceder a los datos en el repositorio de pedido
```c#
    public async Task<IEnumerable<Object>> GetClienteNoPagosYRepresentanteCiudad()
    {
        var results = await (from _cliente in _context.Clientes
                            join _pagos in _context.Pagos on _cliente.Id equals _pagos.IdClienteFk
                            join _empleado in _context.Empleados on _cliente.IdEmpleadoRepresentanteVentasFk equals _empleado.Id
                            join _oficina in _context.Oficinas on _empleado.IdOficinaFk equals _oficina.Id
                            where !_context.Pagos.Any(x=>x.IdClienteFk == _cliente.Id)
                            select new 
                            {
                                IdCliente = _cliente.Id,
                                NombreCliente = _cliente.NombreCliente,
                                NombreRepresentates = _empleado.Nombre,
                                CiudadOficina = _oficina.Ciudad

                            }).ToListAsync();
        return results;
    }
```
Finalmente definimos el endpoind de para el metodo en el controlador de oficina
```c#
    [HttpGet("GetClienteNoPagosYRepresentanteCiudad")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> GetClienteNoPagosYRepresentanteCiudad()
    {
        var results = await _unitOfWork.Clientes.GetClienteNoPagosYRepresentanteCiudad();
        return _mapper.Map<List<Object>>(results);
    }
```c#




