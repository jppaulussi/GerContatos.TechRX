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
            // Inicializa a entidade Regiao com coleções vazias
            _regiao = new Regiao
            {
                DDDs = new List<DDD>() // Inicializa a coleção para evitar NullReferenceException
            };
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

            // Testa a inicialização padrão da coleção DDDs
            Assert.IsNotNull(_regiao.DDDs, "DDDs should be initialized and not null.");

            // Testa a atribuição e recuperação da coleção DDDs
            var ddds = new List<DDD> { new DDD { CodigoDDD = "011" }, new DDD { CodigoDDD = "021" } };
            _regiao.DDDs = ddds;
            Assert.AreEqual(2, _regiao.DDDs.Count, "DDDs should allow setting and getting a collection.");

            // Testa a adição de um novo DDD
            var novoDDD = new DDD { CodigoDDD = "031" };
            _regiao.DDDs.Add(novoDDD);
            Assert.Contains(novoDDD, (System.Collections.ICollection)_regiao.DDDs, "New DDD should be added to the DDDs collection.");
        }

        [Test]
        public void Regiao_Entity_ShouldHandleEmptyCollection()
        {
            // Testa se a coleção DDDs pode ser inicializada como uma lista vazia
            _regiao.DDDs = new List<DDD>();
            Assert.IsEmpty(_regiao.DDDs, "DDDs should be empty when initialized as a new list.");

            // Testa se a coleção DDDs pode ser atribuída como null
            _regiao.DDDs = null;
            Assert.IsNull(_regiao.DDDs, "DDDs can be set to null.");
        }
    }
}