using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.DDD;

public class GetByIdDDDDto : EntityBase
{
    public string? CodigoDDD { get; set; } 
    public int RegiaoId { get; set; }
}
