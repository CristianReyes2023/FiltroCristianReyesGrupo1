using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repositories;
public class OficinaRepository : GenericRepositoryString<Oficina> , IOficina
{
    private readonly JardineriaContext _context;

    public OficinaRepository(JardineriaContext context) : base(context)
    {
        _context = context;
    }
}
