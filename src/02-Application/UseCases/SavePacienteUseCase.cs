using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Application.Helpers;
using PosTech.Hackathon.Pacientes.Application.Requests;
using PosTech.Hackathon.Pacientes.Application.Services.Interfaces;
using PosTech.Hackathon.Pacientes.Domain.Entities;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Domain.Responses;
using System.Text.RegularExpressions;

namespace PosTech.Hackathon.Pacientes.Application.UseCases;

public class SavePacienteUseCase : ISavePacienteUseCase
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAutenticacaoClient _autenticacaoClient;

    public SavePacienteUseCase(IPacienteRepository pacienteRepository, IAutenticacaoClient autenticacaoClient)
    {
        _pacienteRepository = pacienteRepository;
        _autenticacaoClient = autenticacaoClient;
    }
    public async Task<DefaultOutput<PacienteResponse>> SaveNewPacienteAsync(CreatePacienteEvent request)
    {
        // Consumir serviço Authentication e retornar Id para persistir em Collection de Paciente;
        var usuarioRequest = new UsuarioRequest(request.Nome, request.Email, request.Cpf, request.Senha);
        var createUserAutentication = await _autenticacaoClient.SaveAsync(usuarioRequest);
            
        if (createUserAutentication is null) 
        { 
            return new DefaultOutput<PacienteResponse>(System.Net.HttpStatusCode.UnprocessableEntity, false, $"Erro ao cadastrar novo paciente."); 
        }

        var paciente = new Paciente
        {
            Id = createUserAutentication.IdUsuario,
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