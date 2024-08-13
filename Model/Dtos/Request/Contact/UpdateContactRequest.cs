using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Contact;

public class UpdateContactRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome do contato pode conter até 50 caracteres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O preenchimento do telefone é obrigatório")]
    [StringLength(9, MinimumLength = 8, ErrorMessage = "quantidade de caracteres não corresponde a um telefone")]
    public string Telefone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Por favor informar o DDD")]
    public int DDDId { get; set; }
    [Required]
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "Por favor informar o tipo do contato Exemplo: Celular")]
    public int TipoTelefoneId { get; set; }
}
