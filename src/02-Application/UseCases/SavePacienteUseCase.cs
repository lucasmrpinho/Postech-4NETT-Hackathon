using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Entities;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Domain.Responses;
using System.Text.RegularExpressions;

namespace PosTech.Hackathon.Pacientes.Application.UseCases;

public class SavePacienteUseCase : ISavePacienteUseCase
{
    private readonly IPacienteRepository _pacienteRepository;

    public SavePacienteUseCase(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public async Task Execute(Paciente paciente)
    {
        await _pacienteRepository.AddPacienteAsync(paciente);
    }

    public async Task<DefaultOutput<PacienteResponse>> SaveNewPacienteAsync(CreatePacienteEvent request)
    {
        var paciente = new Paciente
        {
            Nome = request.Nome,
            Cpf = NormalizeCpf(request.Cpf),
            Email = request.Email,
            Senha = request.Senha
        };

        await _pacienteRepository.AddPacienteAsync(paciente);

        return new DefaultOutput<PacienteResponse>(System.Net.HttpStatusCode.Created, true, $"Paciente {paciente.Id} cadastrado com sucesso.");
    }
    private string NormalizeCpf(string cpf)
    {
        return cpf != null ? Regex.Replace(cpf, @"[.\-]", "") : string.Empty;
    }
}