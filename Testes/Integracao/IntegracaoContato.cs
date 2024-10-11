using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using GerContatos.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Dtos.Request.Token;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using Business.Mapping;
using Core.Dto.Usuarios;
using GerContatos.API.Services;
using Infrastructure.Data;
using Infrastructure.Repository;

[TestFixture]
public class UsuarioControllerIntegrationTests
{
    private UsuarioController _usuarioController;
    private TokenController _tokenController;
    private IUsuarioService _usuarioService;
    private ITokenService _tokenService;
    private IMapper _mapper;
    private AppDbContext _dbContext;
    private IConfiguration _configuration;

    [SetUp]
    public void SetUp()
    {
        var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        _configuration = new ConfigurationBuilder()
            .SetBasePath(projectRoot)
            .AddJsonFile("appsettings.test.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
            .Options;

        _dbContext = new AppDbContext(dbOptions);
        _dbContext.Database.EnsureCreated();

        // Configuração do AutoMapper
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Defina o seu perfil de mapeamento
        }).CreateMapper();

        // Inicializando os serviços e controllers com injeção de dependência
        _usuarioService = new UsuarioService(new UsuarioRepository(_dbContext), _mapper);
        _tokenService = new TokenService(_configuration, _usuarioService);

        _tokenController = new TokenController(_tokenService, _mapper);
        _tokenController.ControllerContext.HttpContext = new DefaultHttpContext();

        _usuarioController = new UsuarioController(_usuarioService, _mapper);
    }

    // Método para obter o token de autenticação usando um usuário existente no banco
    private async Task<string> GetAuthToken()
    {
        var loginRequest = new GetUsuarioTokenRequest
        {
            Email = "joao.silva@exemplo.com",  // Substitua pelo email do usuário existente no banco
            Password = "senhaSegura123"        // Substitua pela senha do usuário existente no banco
        };

        var result = await _tokenController.Post(loginRequest) as ObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var token = result.Value as string;
        Assert.IsNotNull(token);
        return token;
    }

    [Test]
    public async Task GetUserById_ReturnsOk_WhenUserExists()
    {
        // Arrange
        int userId = 1; // ID do usuário que já existe no banco de dados
        var token = await GetAuthToken();

        _usuarioController.ControllerContext.HttpContext = new DefaultHttpContext();
        _usuarioController.ControllerContext.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

        // Act
        var result = await _usuarioController.GetUserById(userId) as ObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);

        var usuarioDto = result.Value as UsuarioGetByIdDto;
        Assert.IsNotNull(usuarioDto);
        Assert.AreEqual(userId, usuarioDto.RoleId);  // Verifique se o ID corresponde ao usuário esperado
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}
