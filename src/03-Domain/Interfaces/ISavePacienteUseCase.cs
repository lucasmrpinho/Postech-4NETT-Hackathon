using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Responses;

namespace PosTech.Hackathon.Pacientes.Domain.Interfaces
{
    public interface ISavePacienteUseCase
    {
        Task<DefaultOutput<PacienteResponse>> SaveNewPacienteAsync(CreatePacienteEvent request);
    }
}
