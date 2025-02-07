using Microsoft.AspNetCore.Mvc;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using System.Net;

namespace PosTech.Hackathon.Pacientes.API.Controllers;

[ApiController]
[Route("pacientes")]
public class PacientesController : ControllerBase
{
    private readonly ILogger<PacientesController> _logger;
    private readonly ISavePacienteUseCase _useCase;

    public PacientesController(ILogger<PacientesController> logger, ISavePacienteUseCase useCase)
    {
        _logger = logger;
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePacienteEvent request)
    {
        _logger.LogInformation("Recebendo requisição POST em /pacientes");
        var result = await _useCase.SaveNewPacienteAsync(request);
        _logger.LogInformation("Resultado da operação: {Resultado}", result);

        return StatusCode((int)HttpStatusCode.Created, result);
    }
}
