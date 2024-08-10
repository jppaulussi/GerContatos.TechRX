using Core.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testes.Entities
{
    [TestFixture]
    public class RegiaoTestes
    {
        private Regiao _regiao;

        [SetUp]
        public void SetUp()
        {
            _regiao = new Regiao();
        }

        [Test]
        public void Regiao_Entity_ShouldInitializeAndAllowSettingValues()
        {
            // Testa a inicialização padrão de Name
            Assert.IsNull(_regiao.Name, "Name should be null by default.");

            // Testa a atribuição de Name
            var expectedName = "Sudeste";
            _regiao.Name = expectedName;
            Assert.AreEqual(expectedName, _regiao.Name, "Name should allow setting and getting a value.");

            // Testa a inicialização padrão das coleções Contatos e DDDs
            Assert.IsNull(_regiao.Contatos, "Contatos should be null by default.");
            Assert.IsNull(_regiao.DDDs, "DDDs should be null by default.");

            // Testa a atribuição e recuperação da coleção Contatos
            var contatos = new List<Contato> { new Contato { Nome = "Contato1" }, new Contato { Nome = "Contato2" } };
            _regiao.Contatos = contatos;
            Assert.AreEqual(2, _regiao.Contatos.Count, "Contatos should allow setting and getting a collection.");

            // Testa a atribuição e recuperação da coleção DDDs
            var ddds = new List<DDD> { new DDD { CodigoDDD = "011" }, new DDD { CodigoDDD = "021" } };
            _regiao.DDDs = ddds;
            Assert.AreEqual(2, _regiao.DDDs.Count, "DDDs should allow setting and getting a collection.");
        }
    }
}
