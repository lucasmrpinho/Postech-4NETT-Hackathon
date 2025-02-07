using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Application.Helpers;
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

    public async Task<DefaultOutput<PacienteResponse>> SaveNewPacienteAsync(CreatePacienteEvent request)
    {
        var createUserAutentication = Guid.NewGuid(); //consumir serviço Authentication e retornar Id para persistir em Collection de Paciente;

        if (createUserAutentication == Guid.Empty) 
        { 
            return new DefaultOutput<PacienteResponse>(System.Net.HttpStatusCode.UnprocessableEntity, false, $"Erro ao cadastrar novo paciente."); 
        }

        var paciente = new Paciente
        {
            Id = createUserAutentication,
            Nome = request.Nome,
            Cpf = HashHelper.ComputeSha256Hash(NormalizeCpf(request.Cpf)),
            Email = request.Email,
        };

        await _pacienteRepository.AddPacienteAsync(paciente);

        return new DefaultOutput<PacienteResponse>(System.Net.HttpStatusCode.Created, true, $"Paciente {paciente.Id} cadastrado com sucesso.");
    }
    private string NormalizeCpf(string cpf)
    {
        return cpf != null ? Regex.Replace(cpf, @"[.\-]", "") : string.Empty;
    }
}