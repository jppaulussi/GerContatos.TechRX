using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validations;

public class DigitosTelefoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is long numero)
        {
            var numeroString = numero.ToString();
            if (numeroString.Length >= 8 && numeroString.Length <= 9)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Quantidade de caracteres não corresponde a um telefone válido.");
    }
}
