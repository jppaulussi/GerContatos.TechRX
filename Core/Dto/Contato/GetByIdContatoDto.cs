using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Contato;

public class GetByIdContatoDto : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DDDId { get; set; }
    public int UsuarioId { get; set; }
    public int TipoTelefoneId { get; set; }
}
