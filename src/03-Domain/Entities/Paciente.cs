using System.Text.RegularExpressions;

namespace PosTech.Hackathon.Pacientes.Domain.Entities;

public class Paciente
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; }
}
