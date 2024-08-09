using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Usuario : EntityBase
{ 
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public IEnumerable<Contato> Contatos { get; set; }
}
