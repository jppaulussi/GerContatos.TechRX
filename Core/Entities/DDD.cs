using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class DDD : EntityBase
{
    public string CodigoDDD { get; set; } = string.Empty;
    public int RegiaoId { get; set; }
    public  Regiao Regiao { get; set; }
    public ICollection<Contato> Contatos { get; set; }


}
