namespace PosTech.Hackathon.Pacientes.Application.Responses;

public class UsuarioResponse
{
    public Guid IdUsuario { get; set; }

    public string Token { get; set; } = string.Empty;
}
