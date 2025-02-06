using Microsoft.AspNetCore.Mvc;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;

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
    public async Task<ActionResult> Post([FromBody] CreatePacienteEvent request)
    {
        return Ok(await _useCase.SaveNewPacienteAsync(request));
    }
}
