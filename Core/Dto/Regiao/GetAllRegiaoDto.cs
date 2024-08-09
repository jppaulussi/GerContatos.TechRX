using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Regiao;

public class GetAllRegiaoDto : EntityBase
{
    public string Name { get; set; }
}
