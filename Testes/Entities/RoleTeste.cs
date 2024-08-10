using Core.Entities;
using Core.Enums;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testes.Entities
{
    [TestFixture]
    public class RoleTestes
    {
        private Role _role;

        [SetUp]
        public void SetUp()
        {
            _role = new Role();
        }

        [Test]
        public void Role_Entity_ShouldInitializeAndAllowSettingValues()
        {
            // Testa a inicialização padrão de Tipo
            Assert.AreEqual(default(ERole), _role.Tipo, "Tipo should be initialized to its default value.");

            // Testa a atribuição de Tipo
            var expectedTipo = ERole.Administrador;
            _role.Tipo = expectedTipo;
            Assert.AreEqual(expectedTipo, _role.Tipo, "Tipo should allow setting and getting a value.");

            // Testa a inicialização padrão da coleção Usuarios
            Assert.IsNull(_role.Usuarios, "Usuarios should be null by default.");

            // Testa a atribuição e recuperação da coleção Usuarios
            var usuarios = new List<Usuario>
            {
                new Usuario { Name = "Usuario1" },
                new Usuario { Name = "Usuario2" }
            };
            _role.Usuarios = usuarios;
            Assert.AreEqual(2, _role.Usuarios.Count, "Usuarios should allow setting and getting a collection.");
        }
    }
}
