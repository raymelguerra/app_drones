using AppDrones.Api.Controllers;
using AppDrones.Core.Dto;
using AppDrones.Core.Exceptions;
using AppDrones.Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AppDrones.Test
{
    [TestFixture]
    public class DispatchControllerTests
    {
        private Mock<IDrone> mockDrone;
        private Mock<IValidator<RegistryReqDto>> mockRegistryValidator;
        private Mock<IValidator<IEnumerable<LoadMedicationReqDto>>> mockLoadMedicationValidator;
        private DispatchController controller;

        [SetUp]
        public void Setup()
        {
            mockDrone = new Mock<IDrone>();
            mockRegistryValidator = new Mock<IValidator<RegistryReqDto>>();
            mockLoadMedicationValidator = new Mock<IValidator<IEnumerable<LoadMedicationReqDto>>>();
            controller = new DispatchController(mockDrone.Object, mockRegistryValidator.Object, mockLoadMedicationValidator.Object);
        }

        [Test]
        public async Task PostRegistry_ValidInput_ReturnsCreated()
        {
            // Arrange
            var drone = new RegistryReqDto();

            mockRegistryValidator.Setup(v => v.ValidateAsync(It.IsAny<RegistryReqDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            mockDrone.Setup(d => d.Registry(drone)).ReturnsAsync(new RegistryResDto());

            // Act
            var result = await controller.PostRegistry(drone);

            // Assert
            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public async Task PostRegistry_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var drone = new RegistryReqDto();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("field", "error message") });
            // It.IsAny<RegistryReqDto>(), It.IsAny<CancellationToken>())
            mockRegistryValidator.Setup(v => v.ValidateAsync(It.IsAny<RegistryReqDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            // Act
            var result = await controller.PostRegistry(drone);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task PatchLoadingDrone_ValidInput_ReturnsNoContent()
        {
            // Arrange
            var medications = new List<LoadMedicationReqDto>();
            // It.IsAny<RegistryReqDto>(), It.IsAny<CancellationToken>())
            mockLoadMedicationValidator.Setup(v => v.ValidateAsync(It.IsAny< List<LoadMedicationReqDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            mockDrone.Setup(d => d.LoadingMedication(medications, 1)).ReturnsAsync(true);

            // Act
            var result = await controller.PatchLoadingDrone(medications, 1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task PatchLoadingDrone_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var medications = new List<LoadMedicationReqDto>();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("field", "error message") });
            mockLoadMedicationValidator.Setup(v => v.ValidateAsync(It.IsAny<List<LoadMedicationReqDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            // Act
            var result = await controller.PatchLoadingDrone(medications, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task PatchLoadingDrone_ThrowsWeightLimitException_ReturnsBadRequest()
        {
            // Arrange
            var medications = new List<LoadMedicationReqDto>();
            mockLoadMedicationValidator.Setup(v => v.ValidateAsync(It.IsAny<List<LoadMedicationReqDto>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
            mockDrone.Setup(d => d.LoadingMedication(medications, 1)).ThrowsAsync(new WeightLimitException());

            // Act
            var result = await controller.PatchLoadingDrone(medications, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetMedicationsByDrone_ValidInput_ReturnsOk()
        {
            // Arrange
            mockDrone.Setup(d => d.LoadedMedications(1)).ReturnsAsync(new List<LoadedMedicationsResDto>());

            // Act
            var result = await controller.GetMedicationsByDrone(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetMedicationsByDrone_ThrowsDroneNotFoundException_ReturnsBadRequest()
        {
            // Arrange
            mockDrone.Setup(d => d.LoadedMedications(1)).ThrowsAsync(new DroneNotFoundException());

            // Act
            var result = await controller.GetMedicationsByDrone(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}