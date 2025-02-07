using System.Collections.Generic;
using System.Threading.Tasks;
using PosTech.Hackathon.Pacientes.Domain.Entities;

namespace PosTech.Hackathon.Pacientes.Domain.Interfaces;

public interface IPacienteRepository
{
    Task AddPacienteAsync(Paciente paciente);
    Task<Paciente> GetPacienteByIdAsync(string id);
}
