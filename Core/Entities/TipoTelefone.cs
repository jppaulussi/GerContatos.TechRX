using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities;

public class TipoTelefone : EntityBase
{
    public string Tipo { get; set; }

    
    public IEnumerable<Contato> Contatos { get; set; }
}
