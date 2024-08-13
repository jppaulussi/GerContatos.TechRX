using Core.Entities;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;


namespace Core.Tests.Entities
{

    [TestFixture]
    public class ContatoTestes
    {
        private Contato _contato;

        [SetUp]
        public void SetUp()
        {
            _contato = new Contato();
        }

        [Test]
        public void Nome_Required_Validation_ShouldFail_WhenNomeIsEmpty()
        {
            // Arrange
            _contato.Nome = string.Empty;

            // Act
            var validationResults = ValidateModel(_contato);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage == "Nome é obrigatório"));
        }

        [Test]
        public void Nome_MaxLength_Validation_ShouldFail_WhenNomeExceedsMaxLength()
        {
            // Arrange
            _contato.Nome = new string('A', 256); // Exceeds the 255 character limit

            // Act
            var validationResults = ValidateModel(_contato);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage == "O nome pode conter até 255 caracteres"));
        }

        [Test]
        public void Telefone_Required_Validation_ShouldFail_WhenTelefoneIsEmpty()
        {
            // Arrange
            _contato.Telefone = string.Empty;

            // Act
            var validationResults = ValidateModel(_contato);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage == "Telefone é obrigatório"));
        }

        [Test]
        public void Telefone_Length_Validation_ShouldFail_WhenTelefoneIsTooShort()
        {
            // Arrange
            _contato.Telefone = "1234567"; // Less than 8 characters

            // Act
            var validationResults = ValidateModel(_contato);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage == "A telefone deve conter entre 8 e 9 caracteres"));
        }

        [Test]
        public void Telefone_Length_Validation_ShouldFail_WhenTelefoneIsTooLong()
        {
            // Arrange
            _contato.Telefone = "1234567890"; // More than 9 characters

            // Act
            var validationResults = ValidateModel(_contato);

            // Assert
            Assert.IsTrue(validationResults.Any(v => v.ErrorMessage == "A telefone deve conter entre 8 e 9 caracteres"));
        }

        [Test]
        public void Email_Validation_ShouldFail_WhenEmailIsInvalid()
        {
            // Arrange
            var contato = new Contato
            {
                Email = "invalid-email" // E-mail inválido
            };

            // Act
            var context = new ValidationContext(contato, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(contato, context, validationResults, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid, "A validação deve falhar para um e-mail inválido.");
            Assert.IsTrue(validationResults.Any(vr => vr.ErrorMessage == "Formato de e-mail invalido"), "A mensagem de erro para o e-mail inválido está incorreta.");
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}