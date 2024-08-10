using AutoMapper;
using Core.Dto.Usuarios;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;
using GerContatos.API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerContatos.Tests.Services
{
    [TestFixture]
    public class UsuarioServiceTests
    {
        private Mock<IUsuarioRepository> _mockUsuarioRepository;
        private Mock<IMapper> _mockMapper;
        private UsuarioService _usuarioService;

        [SetUp]
        public void Setup()
        {
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _mockMapper = new Mock<IMapper>();
            _usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Create_ShouldReturnSuccess_WhenUsuarioIsCreated()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Name = "John Doe" };
            var dto = new CreateUsuarioDto { Name = "John Doe" };

            _mockMapper.Setup(m => m.Map<CreateUsuarioDto>(It.IsAny<Usuario>())).Returns(dto);
            _mockUsuarioRepository.Setup(r => r.Create(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            // Act
            var response = await _usuarioService.Create(usuario);

            // Assert

            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Usuário cadastrado com sucesso", response.Message);
        }


        [Test]
        public async Task Delete_ShouldReturnSuccess_WhenUsuarioExists()
        {
            // Arrange
            var usuarioId = 1;
            _mockUsuarioRepository.Setup(r => r.GetById(usuarioId)).ReturnsAsync(new Usuario { Id = usuarioId });
            _mockUsuarioRepository.Setup(r => r.Delete(usuarioId)).Returns(Task.CompletedTask);

            // Act
            var response = await _usuarioService.Delete(usuarioId);

            // Assert

            Assert.AreEqual("Usuário excluído com sucesso", response.Message);
        }

        [Test]
        public async Task GetAll_ShouldReturnPagedList_WhenUsuariosExist()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Name = "John Doe" },
                new Usuario { Id = 2, Name = "Jane Doe" }
            };
            var usuariosDto = new List<GetAllUsuarioDto>
            {
                new GetAllUsuarioDto { Name = "John Doe" },
                new GetAllUsuarioDto { Name = "Jane Doe" }
            };

            _mockUsuarioRepository.Setup(r => r.GetAll()).ReturnsAsync(usuarios);
            _mockMapper.Setup(m => m.Map<List<GetAllUsuarioDto>>(usuarios)).Returns(usuariosDto);

            // Act
            var response = await _usuarioService.GetAll();

            // Assert
            Assert.AreEqual(1, response.CurrentPage);
            Assert.AreEqual(25, response.PageSize);
            Assert.AreEqual(usuarios.Count, response.TotalCount);

        }

        [Test]
        public async Task GetById_ShouldReturnUsuario_WhenUsuarioExists()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Name = "John Doe" };
            var usuarioDto = new UsuarioGetByIdDto { Name = "John Doe" };

            _mockUsuarioRepository.Setup(r => r.GetById(usuario.Id)).ReturnsAsync(usuario);
            _mockMapper.Setup(m => m.Map<UsuarioGetByIdDto>(usuario)).Returns(usuarioDto);

            // Act
            var response = await _usuarioService.GetById(usuario.Id);

            // Assert
            Assert.AreEqual(usuarioDto, response.Data);

        }

        [Test]
        public async Task Update_ShouldReturnSuccess_WhenUsuarioIsUpdated()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Name = "John Doe" };
            var dto = new UpdateUsuarioDto { Name = "John Doe Updated" };

            _mockMapper.Setup(m => m.Map<UpdateUsuarioDto>(It.IsAny<Usuario>())).Returns(dto);
            _mockUsuarioRepository.Setup(r => r.Update(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            // Act
            var response = await _usuarioService.Update(usuario);

            // Assert
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Usuário atualizado com sucesso", response.Message);
        }
    }
}
