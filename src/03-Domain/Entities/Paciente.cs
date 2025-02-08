using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PosTech.Hackathon.Pacientes.Domain.Entities;

public class Paciente
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; }
    public string Email { get; set; } = string.Empty;
}
