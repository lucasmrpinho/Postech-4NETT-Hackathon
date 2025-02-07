using MongoDB.Driver;
using PosTech.Hackathon.Pacientes.Domain.Entities;
using PosTech.Hackathon.Pacientes.Domain.Exceptions;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;

namespace PosTech.Hackathon.Pacientes.Infrastructure.Persistence;

public class PacienteRepository : IPacienteRepository
{
    private readonly IMongoCollection<Paciente> _pacienteCollection;

    public PacienteRepository(MongoDbContext context)
    {
        _pacienteCollection = context.GetCollection<Paciente>("Pacientes");
    }

    public async Task AddPacienteAsync(Paciente paciente)
    {
        try
        {
            await _pacienteCollection.InsertOneAsync(paciente);
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new PersistencyException("Já existe um paciente cadastrado com esse CPF.");
        }
    }


    public async Task<List<Paciente>> GetAllPacientesAsync()
    {
        return await _pacienteCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Paciente> GetPacienteByIdAsync(string id)
    {
        return await _pacienteCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}
