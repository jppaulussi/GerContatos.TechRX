using AutoMapper;
using Business.Mapping;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Request.Contact;
using GerContatos.API.Controllers;
using GerContatos.API.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

[TestFixture]
public class ContatoControllerIntegrationTests
{
    private ContatoController _controller;
    private IContatoService _contatoService;
    private IDDDService _dddService;
    private IMapper _mapper;
    private AppDbContext _dbContext;
    private IConfiguration _configuration;

    [SetUp]
    public void SetUp()
    {
        var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName; // Ajuste o caminho conforme a estrutura do seu projeto
        _configuration = new ConfigurationBuilder()
            .SetBasePath(projectRoot)
            .AddJsonFile("appsettings.test.json")
            .Build();
        // Configura a conexão do banco de dados
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _dbContext = new AppDbContext(dbOptions);
        _dbContext.Database.EnsureCreated();

        // Inicializar o mapper
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }).CreateMapper();

        // Inicializar serviços reais aqui
        _dddService = new DDDService(new DDDRepository(_dbContext),_mapper);
        _contatoService = new ContatoService(new ContatoRepository(_dbContext), _mapper, _dddService);

        _controller = new ContatoController(_contatoService, _mapper, _dddService);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task Create_Contato_ReturnsCreated()
    {
        // Arrange
        var createContatoDto = new CreateContactRequest
        {
            Nome = "Teste Contato",
            Telefone = 123456789,
            Email = "teste@exemplo.com",
            DDDId = 1,
            UsuarioId = 1,
            TipoTelefoneId = 1
        };

        // Act
        var result = await _controller.Create(createContatoDto) as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        Assert.AreEqual("GetById", result.ActionName);
        Assert.IsNotNull(result.Value);

        var contato = result.Value as Contato;
        Assert.IsNotNull(contato);
        Assert.AreEqual("Teste Contato", contato.Nome);

        // Verifique se o contato foi realmente adicionado ao banco de dados
        var savedContato = await _dbContext.Contato.FindAsync(contato.Id);
        Assert.IsNotNull(savedContato);
        Assert.AreEqual("Teste Contato", savedContato.Nome);
    }
}
