using Core.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Validations;

namespace Core.Request.Contact;

public class CreateContactRequest
{

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome do contato pode conter até 50 caracteres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O preenchimento do telefone é obrigatório")]
    [DigitosTelefone]
    public long Telefone { get; set; }
    [EmailAddress(ErrorMessage = "Formato de e-mail invalido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Por favor informar o DDD")]
    public int DDDId { get; set; }
    [Required(ErrorMessage = "Por favor informar o usuário responsável pelo contato")]
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "Por favor informar o tipo do contato Exemplo: Celular")]
    public int TipoTelefoneId { get; set; }


}
