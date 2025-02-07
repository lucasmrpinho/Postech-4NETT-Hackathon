using System.Net;

namespace PosTech.Hackathon.Pacientes.Domain.Exceptions;

public class PersistencyException : Exception
{
    public int StatusCode { get; } = (int)HttpStatusCode.Conflict;

    public PersistencyException(string message) : base(message) { }
}
