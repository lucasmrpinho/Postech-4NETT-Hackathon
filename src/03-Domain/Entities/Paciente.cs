using System.Text.RegularExpressions;

namespace PosTech.Hackathon.Pacientes.Domain.Entities;

public class Paciente
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; }
    public string Email { get; set; } = string.Empty;
}
