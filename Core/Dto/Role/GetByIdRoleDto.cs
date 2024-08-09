using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Role;

public class GetByIdRoleDto : EntityBase
{
    public ERole Tipo { get; set; }
}
