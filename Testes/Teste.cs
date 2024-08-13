using NUnit.Framework;
using Moq;
using AutoMapper;
using Core.Dto.Contato;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;
using GerContatos.API.Services;
using System.Threading.Tasks;
using Core.Dto.DDD;


namespace GerContatos.Tests
{
    [TestFixture]
    public class ContatoServiceTests
    {
        private Mock<IContatoRepository> _mockContatoRepository;
        private Mock<IDDDService> _mockDDDService;
        private IMapper _mapper;
        private ContatoService _contatoService;

        [SetUp]
        public void Setup()
        {
            _mockContatoRepository = new Mock<IContatoRepository>();
            _mockDDDService = new Mock<IDDDService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Configurar mapeamentos aqui, se necessário
                cfg.CreateMap<Contato, CreateContatoDto>();
                cfg.CreateMap<CreateContatoDto, Contato>();
                cfg.CreateMap<DDD, GetByIdDDDDto>();
                // Adicione outros mapeamentos conforme necessário
            });
            _mapper = mapperConfig.CreateMapper();

            _contatoService = new ContatoService(_mockContatoRepository.Object, _mapper, _mockDDDService.Object);
        }




        [Test]
        public async Task Create_InvalidDDD_ReturnsNotFound()
        {
            // Arrange
            var contato = new Contato { Id = 0, DDDId = 999, /* outras propriedades */ };

            _mockDDDService.Setup(s => s.GetById(999))
                .ReturnsAsync(new Response<GetByIdDDDDto>(null, 404, "DDD não encontrado"));

            // Act
            var result = await _contatoService.Create(contato);

            // Assert
            Assert.NotNull(result);
       
            Assert.AreEqual("DDD não encontrado", result.Message);
            Assert.IsNull(result.Data);
        }

        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Adicione o profile atualizado
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }


    }
}
