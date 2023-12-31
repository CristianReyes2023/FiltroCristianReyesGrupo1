using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class DetallePedidoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DetallePedidoController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var results = await _unitOfWork.DetallePedidos.GetAllAsync();
        return _mapper.Map<List<DetallePedidoDto>>(results);
    }

    [HttpGet("{idpedido}/{idproducto}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Get(int idpedido,string idproducto)
    {
        var result = await _unitOfWork.DetallePedidos.GetByIdAsync(idpedido,idproducto);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<DetallePedidoDto>(result);
    }

    [HttpPost] // 2611
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedidoDto>> Post(DetallePedidoDto resultDto)
    {
        var result = _mapper.Map<DetallePedido>(resultDto);
        _unitOfWork.DetallePedidos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
        resultDto.IdPedidoFk = result.IdPedidoFk;
        resultDto.IdProductoFk = result.IdProductoFk;
        return CreatedAtAction(nameof(Post), new { idpedido = resultDto.IdPedidoFk,idproducto = resultDto.IdProductoFk }, resultDto);
    }

    [HttpPut("{idpedido}/{idproducto}")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Put(int idpedido,string idproducto, [FromBody] DetallePedidoDto resultDto)
    {
        var exists = await _unitOfWork.DetallePedidos.GetByIdAsync(idpedido,idproducto);
        if (exists == null)
        {
            return NotFound();
        }
        // if (resultDto.Id == 0)
        // {
        //     resultDto.Id = id;
        // }
        // if (resultDto.Id != id)
        // {
        //     return BadRequest();
        // }
        // Update the properties of the existing entity with values from resultDto
        _mapper.Map(resultDto, exists);
        // if (resultDto.FechaCreacion == DateOnly.MinValue)
        // {
        //     exists.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        // }
        // The context is already tracking result, so no need to attach it
        await _unitOfWork.SaveAsync();
        // Return the updated entity
        return _mapper.Map<DetallePedidoDto>(exists);
    }

    [HttpDelete("{idpedido}/{idproducto}")] // 2611
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int idpedido,string idproducto)
    {
        var result = await _unitOfWork.DetallePedidos.GetByIdAsync(idpedido,idproducto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.DetallePedidos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("Get20ProductosMásVendidos")] // 2611
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> Get20ProductosMásVendidos()
    {
        var results = await _unitOfWork.DetallePedidos.Get20ProductosMásVendidos();
        return _mapper.Map<List<Object>>(results);
    }
}