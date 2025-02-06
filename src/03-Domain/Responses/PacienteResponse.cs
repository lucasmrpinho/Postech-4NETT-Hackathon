namespace PosTech.Hackathon.Pacientes.Domain.Responses;

public class PacienteResponse
{
    public PacienteResponse(string messege)
    {
        Message = messege;
    }
    public string Message { get; set; }
}
