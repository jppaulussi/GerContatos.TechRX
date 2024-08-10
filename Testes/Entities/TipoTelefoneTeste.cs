using Core.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testes.Entities
{
    [TestFixture]
    public class TipoTelefoneTestes
    {
        private TipoTelefone _tipoTelefone;

        [SetUp]
        public void SetUp()
        {
            _tipoTelefone = new TipoTelefone();
        }

        [Test]
        public void TipoTelefone_Entity_ShouldInitializeAndAllowSettingValues()
        {
            // Testa a inicialização padrão de Tipo
            Assert.IsNull(_tipoTelefone.Tipo, "Tipo should be null by default.");

            // Testa a atribuição de Tipo
            var expectedTipo = "Celular";
            _tipoTelefone.Tipo = expectedTipo;
            Assert.AreEqual(expectedTipo, _tipoTelefone.Tipo, "Tipo should allow setting and getting a value.");

            // Testa a inicialização padrão da coleção Contatos
            Assert.IsNull(_tipoTelefone.Contatos, "Contatos should be null by default.");

            // Testa a atribuição e recuperação da coleção Contatos
            var contatos = new List<Contato>
            {
                new Contato { Nome = "Contato1" },
                new Contato { Nome = "Contato2" }
            };
            _tipoTelefone.Contatos = contatos;
            Assert.AreEqual(2, _tipoTelefone.Contatos.Count(), "Contatos should allow setting and getting a collection.");
        }
    }
}
