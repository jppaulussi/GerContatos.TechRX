using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums;

public enum ERole
{
    [Description("Administrador")]
    Administrador = 1,

    [Description("Usuario")]
    Usuario = 2
}


