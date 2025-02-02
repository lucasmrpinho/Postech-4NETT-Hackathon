using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PosTech.Hackathon.Pacientes.Domain.Responses
{
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
    }
}
