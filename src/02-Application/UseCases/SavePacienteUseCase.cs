using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Application.Validators;
using PosTech.Hackathon.Pacientes.Domain.Exceptions;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Domain.Responses;

namespace PosTech.Hackathon.Pacientes.Application.UseCases;

public class SavePacienteUseCase : ISavePacienteUseCase
{
    public async Task<DefaultOutput<PacienteResponse>> SaveNewPacienteAsync(CreatePacienteEvent request)
    {

        var validator = new PacienteValidator();
        var validation = validator.Validate(request);
        if (!validation.IsValid)
        {
            var error = validation.Errors.ToList().First();
            throw new DomainException(error.ErrorMessage);
        }
        return null;
        //var published = await _publisher.PublishAsync(request);
        //return new DefaultOutput<PacienteResponse>(published, new PacienteResponse("Registro salvo com sucesso"));

    }
}
