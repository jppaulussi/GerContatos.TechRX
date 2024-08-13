using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.User;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(255,ErrorMessage = "O nome pode conter até 255 caracteres")]
    public string Name { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatório")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O preenchimento da senha é obrigatório")]
    [StringLength(16,MinimumLength = 8,ErrorMessage = "A senha deve conter entre 8 e 16 caracteres")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Obrigatório o tipo do Papel do usuário")]
    public int RoleId { get; set; }
}
