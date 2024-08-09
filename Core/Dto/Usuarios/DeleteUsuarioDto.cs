using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Usuarios;

public class DeleteUsuarioDto : EntityBase
{
    public string Name { get; set; } = string.Empty;
}
