using AutoMapper;
using Core.Dto.DDD;
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
    public class DDDServiceTests
    {
        private Mock<IDDDRepository> _mockDddRepository;
        private Mock<IMapper> _mockMapper;
        private DDDService _dddService;

        [SetUp]
        public void SetUp()
        {
            _mockDddRepository = new Mock<IDDDRepository>();
            _mockMapper = new Mock<IMapper>();
            _dddService = new DDDService(_mockDddRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Create_ShouldReturnSuccess_WhenValidDataIsProvided()
        {
            // Arrange
            var ddd = new DDD { CodigoDDD = "11" };
            var dto = new CreateDDDDto();

            _mockMapper.Setup(m => m.Map<CreateDDDDto>(ddd)).Returns(dto);
            _mockDddRepository.Setup(r => r.Create(ddd)).Returns(Task.CompletedTask);

            // Act
            var result = await _dddService.Create(ddd);

            // Assert
 
            Assert.AreEqual("DDD criado com sucesso", result.Message);
            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task Delete_ShouldReturnNotFound_WhenDDDDoesNotExist()
        {
            // Arrange
            int dddId = 1;
            _mockDddRepository.Setup(r => r.GetById(dddId)).ReturnsAsync((DDD)null);

            // Act
            var result = await _dddService.Delete(dddId);

            // Assert

            Assert.AreEqual("DDD não encontrado", result.Message);
        }

        [Test]
        public async Task Delete_ShouldReturnSuccess_WhenDDDExists()
        {
            // Arrange
            int dddId = 1;
            var ddd = new DDD { Id = dddId };
            _mockDddRepository.Setup(r => r.GetById(dddId)).ReturnsAsync(ddd);
            _mockDddRepository.Setup(r => r.Delete(dddId)).Returns(Task.CompletedTask);

            // Act
            var result = await _dddService.Delete(dddId);

            // Assert

            Assert.AreEqual("DDD excluído com sucesso", result.Message);
        }

        [Test]
        public async Task GetAll_ShouldReturnPagedResponse_WhenDataIsAvailable()
        {
            // Arrange
            var ddds = new List<DDD> { new DDD() };
            var dtoList = new List<GetAllDDDDto> { new GetAllDDDDto() };
            _mockDddRepository.Setup(r => r.GetAll()).ReturnsAsync(ddds);
            _mockMapper.Setup(m => m.Map<IList<GetAllDDDDto>>(ddds)).Returns(dtoList);

            // Act
            var result = await _dddService.GetAll();

            // Assert
            Assert.AreEqual(1, result.TotalCount);
            Assert.AreEqual(dtoList, result.Data);
        }

        [Test]
        public async Task GetById_ShouldReturnSuccess_WhenDDDExists()
        {
            // Arrange
            int dddId = 1;
            var ddd = new DDD { Id = dddId };
            var dto = new GetByIdDDDDto();
            _mockDddRepository.Setup(r => r.GetById(dddId)).ReturnsAsync(ddd);
            _mockMapper.Setup(m => m.Map<GetByIdDDDDto>(ddd)).Returns(dto);

            // Act
            var result = await _dddService.GetById(dddId);

            // Assert

            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task GetById_ShouldReturnNotFound_WhenDDDDoesNotExist()
        {
            // Arrange
            int dddId = 1;
            _mockDddRepository.Setup(r => r.GetById(dddId)).ReturnsAsync((DDD)null);

            // Act
            var result = await _dddService.GetById(dddId);

            // Assert

            Assert.AreEqual("DDD não encontrado", result.Message);
        }

        [Test]
        public async Task Update_ShouldReturnSuccess_WhenDDDExists()
        {
            // Arrange
            var ddd = new DDD { Id = 1, CodigoDDD = "11" };
            var dto = new UpdateDDDDto();
            _mockDddRepository.Setup(r => r.GetById(ddd.Id)).ReturnsAsync(ddd);
            _mockDddRepository.Setup(r => r.Update(ddd)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<UpdateDDDDto>(ddd)).Returns(dto);

            // Act
            var result = await _dddService.Update(ddd);

            // Assert

            Assert.AreEqual("DDD atualizado com sucesso", result.Message);
            Assert.AreEqual(dto, result.Data);
        }

        [Test]
        public async Task Update_ShouldReturnNotFound_WhenDDDDoesNotExist()
        {
            // Arrange
            var ddd = new DDD { Id = 1 };
            _mockDddRepository.Setup(r => r.GetById(ddd.Id)).ReturnsAsync((DDD)null);

            // Act
            var result = await _dddService.Update(ddd);

            // Assert

            Assert.AreEqual("DDD não encontrado", result.Message);
        }
    }
}
