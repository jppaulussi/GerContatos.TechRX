using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class Contato : EntityBase
{

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(255, ErrorMessage = "O nome pode conter até 255 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [StringLength(9, MinimumLength = 8, ErrorMessage = "A telefone deve conter entre 8 e 9 caracteres")]
    public string Telefone { get; set; } = string.Empty;
    [EmailAddress(ErrorMessage = "Formato de e-mail invalido")]
    public string Email { get; set; } = string.Empty;
    public int DDDId { get; set; }
    public int UsuarioId { get; set; }
    public int TipoTelefoneId { get; set; }


    public DDD Ddd { get; set; } = null;
    public Usuario Usuario { get; set; } = null;
    public TipoTelefone TipoTelefone { get; set; } = null;

}
