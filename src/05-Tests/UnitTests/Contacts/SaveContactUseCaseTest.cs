using Bogus;
using FluentAssertions;
using Moq;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Application.UseCases;
using PosTech.Hackathon.Pacientes.Domain.Exceptions;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Domain.Responses;
using static PosTech.Hackathon.Pacientes.Domain.Utils.ErrorMessageHelper;

namespace PosTech.Hackathon.Pacientes.UnitTests.Pacientes;

public class SavePacienteUseCaseTest
{

    private readonly Faker _faker = new("pt_BR");

    [Fact(DisplayName = "Registrar um novo paciente")]
    [Trait("Action", "Handle")]
    public async Task UseCase_NewPacienteRegistration_ShouldRegisterWithSuccess()
    {
        // Arrange
        var request = GetRequest();
        Mock<ISavePacientePublisher> publisher = new();
        publisher.Setup(c => c.PublishAsync(request)).ReturnsAsync(true);;

        SavePacienteUseCase useCase = new(publisher.Object);

        // Act
        DefaultOutput<PacienteResponse> output = await useCase.SaveNewPacienteAsync(request);

        // Assert
        output.Success.Should().BeTrue();
        output.Data.Should().NotBeNull();
        
    }

    ////CONTACT002
    [Theory(DisplayName = "Email inválido")]
    [Trait("Action", "UseCase")]
    [InlineData("teste mail@gmail.com")]
    [InlineData("teste.mail@")]
    [InlineData("teste.mail9@hotmail")]
    [InlineData("teste.com")]
    [InlineData("@teste.com")]
    [InlineData("")]
    [InlineData(" ")]
    public async Task UseCase_EmailInvalid_ShouldError(string email)
    {
        // Arrange
        var request = GetRequest();
        request.EmailPaciente = email;

        var publisher = GetMockPublisher(request);

        SavePacienteUseCase useCase = new(publisher.Object);

        // Act            
        DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewPacienteAsync(request));

        // Assert
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be(PACIENTE001);                     
    }
    
    [Theory(DisplayName = "CPF inválido")]
    [Trait("Action", "UseCase")]
    [InlineData("12345678999")]
    [InlineData("123123123123123")]
    [InlineData("00000000000")]
    [InlineData("87654321A6")]
    [InlineData("8#76543212")]
    [InlineData("(123)456-7")]
    [InlineData("")]
    [InlineData(" ")]
    public async Task UseCase_CPFInvalid_ShouldError(string cpf)
    {
        // Arrange
        var request = GetRequest();
        request.CPFPaciente = cpf;

        var publisher = GetMockPublisher(request);

        SavePacienteUseCase useCase = new(publisher.Object);

        // Act            
        DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewPacienteAsync(request));

        // Assert
        exception.Message.Should().NotBeNullOrEmpty();
        exception.Message.Should().Be(PACIENTE002);
    }
 
    private Mock<ISavePacientePublisher>  GetMockPublisher(CreatePacienteEvent request)
    {
        Mock<ISavePacientePublisher> publisher = new();
        publisher.Setup(c => c.PublishAsync(request)).ReturnsAsync(true);
        return publisher;
    }
    private CreatePacienteEvent GetRequest()
    {
        return new()
        {
            CPFPaciente = "72056525020",
            NomePaciente = _faker.Name.FirstName(),
            EmailPaciente = _faker.Internet.Email(),
            SenhaPaciente = "Abcd7894",
        };
    }
}
