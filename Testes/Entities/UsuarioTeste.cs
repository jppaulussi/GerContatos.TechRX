using Core.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testes.Entities
{
    [TestFixture]
    public class UsuarioTestes
    {
        private Usuario _usuario;

        [SetUp]
        public void SetUp()
        {
            _usuario = new Usuario();
        }

        [Test]
        public void Usuario_Entity_ShouldInitializeAndAllowSettingValues()
        {
            // Testa a inicialização padrão das propriedades
            Assert.AreEqual(string.Empty, _usuario.Name, "Name should be initialized to an empty string.");
            Assert.AreEqual(string.Empty, _usuario.Email, "Email should be initialized to an empty string.");
            Assert.AreEqual(string.Empty, _usuario.Password, "Password should be initialized to an empty string.");
            Assert.AreEqual(0, _usuario.RoleId, "RoleId should be initialized to 0.");
            Assert.IsNull(_usuario.Role, "Role should be null by default.");
            Assert.IsNull(_usuario.Contatos, "Contatos should be null by default.");

            // Testa a atribuição de valores simples
            var expectedName = "John Doe";
            _usuario.Name = expectedName;
            Assert.AreEqual(expectedName, _usuario.Name, "Name should allow setting and getting a value.");

            var expectedEmail = "johndoe@example.com";
            _usuario.Email = expectedEmail;
            Assert.AreEqual(expectedEmail, _usuario.Email, "Email should allow setting and getting a value.");

            var expectedPassword = "Password123";
            _usuario.Password = expectedPassword;
            Assert.AreEqual(expectedPassword, _usuario.Password, "Password should allow setting and getting a value.");

            var expectedRoleId = 1;
            _usuario.RoleId = expectedRoleId;
            Assert.AreEqual(expectedRoleId, _usuario.RoleId, "RoleId should allow setting and getting a value.");

            // Testa a atribuição da Role
            var expectedRole = new Role { Tipo = Core.Enums.ERole.Administrador };
            _usuario.Role = expectedRole;
            Assert.AreEqual(expectedRole, _usuario.Role, "Role should allow setting and getting a value.");

            // Testa a atribuição e recuperação da coleção Contatos
            var contatos = new List<Contato>
            {
                new Contato { Nome = "Contato1" },
                new Contato { Nome = "Contato2" }
            };
            _usuario.Contatos = contatos;
            Assert.AreEqual(2, _usuario.Contatos.Count(), "Contatos should allow setting and getting a collection.");
        }
    }
}
