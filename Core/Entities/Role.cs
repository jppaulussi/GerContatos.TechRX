using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Role : EntityBase
{ 
    public ERole Tipo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; }
}
