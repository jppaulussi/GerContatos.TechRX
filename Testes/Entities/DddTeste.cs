using Core.Entities;
using NUnit.Framework;

namespace Testes.Entities
{
    [TestFixture]
    public class DDDTestes
    {
       
        private DDD _ddd;

        [SetUp]
        public void SetUp()
        {
            _ddd = new DDD();
        }

        [Test]
        public void DDD_Entity_ShouldInitializeAndAllowSettingValues()
        {
            // Testa a inicialização padrão de CodigoDDD
            Assert.AreEqual(string.Empty, _ddd.CodigoDDD, "CodigoDDD should be initialized as an empty string.");

            // Testa a atribuição de CodigoDDD
            var expectedCodigo = "011";
            _ddd.CodigoDDD = expectedCodigo;
            Assert.AreEqual(expectedCodigo, _ddd.CodigoDDD, "CodigoDDD should allow setting and getting a value.");

            // Testa a atribuição de RegiaoId
            var expectedRegiaoId = 1;
            _ddd.RegiaoId = expectedRegiaoId;
            Assert.AreEqual(expectedRegiaoId, _ddd.RegiaoId, "RegiaoId should allow setting and getting a value.");

            // Testa a inicialização padrão da propriedade de navegação Regiao
            Assert.IsNull(_ddd.Regiao, "Regiao should be null by default.");

            // Testa a atribuição de Regiao
            var expectedRegiao = new Regiao { Name = "Sudeste" };
            _ddd.Regiao = expectedRegiao;
            Assert.AreEqual(expectedRegiao, _ddd.Regiao, "Regiao should allow setting and getting a value.");
            Assert.AreEqual("Sudeste", _ddd.Regiao.Name, "Regiao.Nome should match the expected value.");
        }
    }
}
