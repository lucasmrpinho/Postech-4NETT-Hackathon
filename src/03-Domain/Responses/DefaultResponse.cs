using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;

namespace PosTech.Hackathon.Pacientes.Domain.Responses;

/// <summary>
/// Default return object from endpoints.
/// </summary>
[ExcludeFromCodeCoverage]
public record DefaultOutput<T>
{
    public DefaultOutput() { }

    public DefaultOutput(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public DefaultOutput(bool success, T data)
    {
        Success = success;
        Data = data;
    }
    public DefaultOutput(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public DefaultOutput(HttpStatusCode statusCode, bool success, string message)
    {
        StatusCode = statusCode;
        Success = success;
        Message = message;
    }

    /// <summary>
    /// Indicates whether the requested processing was completed successfully.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; } = true;

    /// <summary>
    /// Message that describes the result of processing.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Set of data returned from the requested processing.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    /// <summary>
    /// Set of StatusCode returned from the requested processing.
    /// </summary>
    [JsonPropertyName("statusCode")]
    public HttpStatusCode StatusCode { get; init; }
}
