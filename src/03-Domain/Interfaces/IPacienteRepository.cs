using PosTech.Hackathon.Pacientes.Domain.Entities;

namespace PosTech.Hackathon.Pacientes.Domain.Interfaces;

public interface IPacienteRepository
{
    Task AddPacienteAsync(Paciente paciente);
}
