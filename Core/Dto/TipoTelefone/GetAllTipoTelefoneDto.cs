using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.TipoTelefone;

public class GetAllTipoTelefoneDto : EntityBase
{
    public string Tipo { get; set; }
}
