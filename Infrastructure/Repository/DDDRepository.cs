using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class DDDRepository : EFRepository<DDD>, IDDDRepository
{
    public DDDRepository(AppDbContext context) : base(context)
    {
    }
}
