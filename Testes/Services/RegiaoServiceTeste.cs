using AutoMapper;
using Core.Dto.Regiao;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Responses;
using GerContatos.API.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerContatos.API.Tests
{
    [TestFixture]
    public class RegiaoServiceTests
    {
        private Mock<IRegiaoRepository> _mockRegiaoRepository;
        private Mock<IMapper> _mockMapper;
        private RegiaoService _regiaoService;

        [SetUp]
        public void SetUp()
        {
            _mockRegiaoRepository = new Mock<IRegiaoRepository>();
            _mockMapper = new Mock<IMapper>();
            _regiaoService = new RegiaoService(_mockRegiaoRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Create_ShouldReturnSuccess_WhenValidDataIsProvided()
        {
            // Arrange
            var regiao = new Regiao { Name = "Região A" };
            var dto = new CreateRegiaoDto();

            _mockMapper.Setup(m => m.Map<CreateRegiaoDto>(regiao)).Returns(dto);
            _mockRegiaoRepository.Setup(r => r.Create(regiao)).Returns(Task.CompletedTask);

            // Act
            var result = await _regiaoService.Create(regiao);

            // Assert

            Assert.AreEqual("Região criada com sucesso", result.Message);
            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task Delete_ShouldReturnNotFound_WhenRegiaoDoesNotExist()
        {
            // Arrange
            int regiaoId = 1;
            _mockRegiaoRepository.Setup(r => r.GetById(regiaoId)).ReturnsAsync((Regiao)null);

            // Act
            var result = await _regiaoService.Delete(regiaoId);

            // Assert

            Assert.AreEqual("Região não encontrada", result.Message);
        }

        [Test]
        public async Task Delete_ShouldReturnSuccess_WhenRegiaoExists()
        {
            // Arrange
            int regiaoId = 1;
            var regiao = new Regiao { Id = regiaoId };
            _mockRegiaoRepository.Setup(r => r.GetById(regiaoId)).ReturnsAsync(regiao);
            _mockRegiaoRepository.Setup(r => r.Delete(regiaoId)).Returns(Task.CompletedTask);

            // Act
            var result = await _regiaoService.Delete(regiaoId);

            // Assert

            Assert.AreEqual("Região excluída com sucesso", result.Message);
        }

        [Test]
        public async Task GetAll_ShouldReturnPagedResponse_WhenDataIsAvailable()
        {
            // Arrange
            var regioes = new List<Regiao> { new Regiao { Name = "Região A" } };
            var dtoList = new List<GetAllRegiaoDto> { new GetAllRegiaoDto() };
            _mockRegiaoRepository.Setup(r => r.GetAll()).ReturnsAsync(regioes);
            _mockMapper.Setup(m => m.Map<IList<GetAllRegiaoDto>>(regioes)).Returns(dtoList);

            // Act
            var result = await _regiaoService.GetAll();

            // Assert
            Assert.AreEqual(1, result.TotalCount);
            Assert.AreEqual(dtoList, result.Data);
        }

        [Test]
        public async Task GetById_ShouldReturnSuccess_WhenRegiaoExists()
        {
            // Arrange
            int regiaoId = 1;
            var regiao = new Regiao { Id = regiaoId };
            var dto = new GetByIdRegiaoDto();
            _mockRegiaoRepository.Setup(r => r.GetById(regiaoId)).ReturnsAsync(regiao);
            _mockMapper.Setup(m => m.Map<GetByIdRegiaoDto>(regiao)).Returns(dto);

            // Act
            var result = await _regiaoService.GetById(regiaoId);

            // Assert

            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task GetById_ShouldReturnNotFound_WhenRegiaoDoesNotExist()
        {
            // Arrange
            int regiaoId = 1;
            _mockRegiaoRepository.Setup(r => r.GetById(regiaoId)).ReturnsAsync((Regiao)null);

            // Act
            var result = await _regiaoService.GetById(regiaoId);

            // Assert

            Assert.AreEqual("Região não encontrada", result.Message);
        }

        [Test]
        public async Task Update_ShouldReturnSuccess_WhenRegiaoExists()
        {
            // Arrange
            var regiao = new Regiao { Id = 1, Name = "Região Atualizada" };
            var dto = new UpdateRegiaoDto();
            _mockRegiaoRepository.Setup(r => r.GetById(regiao.Id)).ReturnsAsync(regiao);
            _mockRegiaoRepository.Setup(r => r.Update(regiao)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<UpdateRegiaoDto>(regiao)).Returns(dto);

            // Act
            var result = await _regiaoService.Update(regiao);

            // Assert

            Assert.AreEqual("Região atualizada com sucesso", result.Message);
            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task Update_ShouldReturnNotFound_WhenRegiaoDoesNotExist()
        {
            // Arrange
            var regiao = new Regiao { Id = 1 };
            _mockRegiaoRepository.Setup(r => r.GetById(regiao.Id)).ReturnsAsync((Regiao)null);

            // Act
            var result = await _regiaoService.Update(regiao);

            // Assert
            Assert.AreEqual("Região não encontrada", result.Message);
        }
    }
}
