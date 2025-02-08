using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;

namespace PosTech.Hackathon.Pacientes.Domain.Interfaces;

public interface ISavePacientePublisher
{
    public Task<bool> PublishAsync(CreatePacienteEvent request);
}
