using Core.Interfaces.Repositories;
using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class TipoTelefoneRepository : EFRepository<TipoTelefone>, ITipoTelefoneRepository
{
    public TipoTelefoneRepository(AppDbContext context) : base(context)
    {
    }
}
