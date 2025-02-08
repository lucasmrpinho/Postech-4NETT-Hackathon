using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PosTech.Hackathon.Pacientes.Infrastructure.Config;
using PosTech.Hackathon.Pacientes.Domain.Entities;

namespace PosTech.Hackathon.Pacientes.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        CreateUniqueIndex();
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    private void CreateUniqueIndex()
    {
        var collection = GetCollection<Paciente>("Pacientes");

        var indexKeys = Builders<Paciente>.IndexKeys.Ascending(p => p.Cpf);
        var indexOptions = new CreateIndexOptions { Unique = true, Sparse = false };

        var indexModel = new CreateIndexModel<Paciente>(indexKeys, indexOptions);

        try
        {
            var indexes = collection.Indexes.List().ToList();
            bool indexExists = indexes.Any(index => index["name"] == "Cpf_1");

            if (!indexExists)
            {
                collection.Indexes.CreateOne(indexModel);
                Console.WriteLine("Índice único criado para CPF.");
            }
            else
            {
                Console.WriteLine("Índice já existente para CPF.");
            }
        }
        catch (MongoCommandException ex)
        {
            Console.WriteLine($"Erro ao criar índice único para CPF: {ex.Message}");
        }
    }

}
