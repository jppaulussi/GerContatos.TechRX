using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Usuarios;

public class UsuarioGetByIdDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
}
