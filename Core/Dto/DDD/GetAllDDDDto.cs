using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.DDD;

public class GetAllDDDDto : EntityBase
{
    public string CodigoDDD { get; set; } = string.Empty;
    public int RegiaoId { get; set; }
}
