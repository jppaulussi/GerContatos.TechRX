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
    public async Task SetUp()
    {
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
              .UseSqlServer("Server=localhost,1433;Database=TestDb;User Id=sa;Password=SenhaSegura123!;TrustServerCertificate=True;")
              .Options;

        _dbContext = new AppDbContext(dbOptions);

        // Certifique-se de que o banco de dados é criado e aplicar as migrações
        await _dbContext.Database.EnsureCreatedAsync();
        await _dbContext.Database.MigrateAsync();  // Aplicar as migrações se estiver usando EF Core

        // Configuração do AutoMapper
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }).CreateMapper();

        // Inicializando os serviços e controllers com injeção de dependência
        _usuarioService = new UsuarioService(new UsuarioRepository(_dbContext), _mapper);
        _tokenService = new TokenService(_configuration, _usuarioService);

        _tokenController = new TokenController(_tokenService, _mapper);
        _tokenController.ControllerContext.HttpContext = new DefaultHttpContext();

        _usuarioController = new UsuarioController(_usuarioService, _mapper);

        // Inserindo um papel para o usuário
        var papel = new Role
        {
            Tipo = Core.Enums.ERole.Administrador // Definindo um papel
        };
        _dbContext.Add(papel);
        await _dbContext.SaveChangesAsync();

        // Inserindo um usuário para autenticação
        var usuario = new Usuario
        {
            Email = "joao.silva@exemplo.com",
            Password = "senhaSegura123", // Ajuste o hash conforme necessário
            RoleId = papel.Id // Usando o ID do papel inserido
        };
        _dbContext.Usuario.Add(usuario);
        await _dbContext.SaveChangesAsync();
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
