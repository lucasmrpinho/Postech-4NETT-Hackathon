using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PosTech.Hackathon.Pacientes.Application.Requests;

public class SaidaPadrao<T>
{
    public SaidaPadrao() { }

    public SaidaPadrao(bool sucesso, string mensagem, T data)
    {
        this.Sucesso = sucesso;
        Mensagem = mensagem;
        Dados = data;
    }

    public SaidaPadrao(bool sucesso, T data)
    {
        Sucesso = sucesso;
        Dados = data;
    }

    public SaidaPadrao(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }

    /// <summary>
    /// Indica se o processamento solicitado foi concluído com sucesso.
    /// </summary>
    [JsonPropertyName("sucesso")]
    public bool Sucesso { get; init; }

    /// <summary>
    /// Mensagem que descreve o resultado do processamento.
    /// </summary>
    [JsonPropertyName("mensagem")]
    public string Mensagem { get; init; }

    /// <summary>
    /// Conjunto de dados retornados do processamento solicitado.
    /// </summary>
    [JsonPropertyName("data")]
    public T Dados { get; init; }
}

/// <summary>
/// Retorno padrão dos endpoints.
/// </summary>
[ExcludeFromCodeCoverage]
public record SaidaPadrao
{
    public SaidaPadrao() { }

    public SaidaPadrao(bool sucesso, string mensagem, object dados)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        Dados = dados;
    }

    public SaidaPadrao(bool sucesso, object dados)
    {
        Sucesso = sucesso;
        Dados = dados;
    }

    public SaidaPadrao(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }

    /// <summary>
    /// Indica se o processamento solicitado foi concluído com sucesso.
    /// </summary>
    [JsonPropertyName("sucesso")]
    public bool Sucesso { get; init; }

    /// <summary>
    /// Mensagem que descreve o resultado do processamento.
    /// </summary>
    [JsonPropertyName("mensagem")]
    public string Mensagem { get; init; }

    /// <summary>
    /// Conjunto de dados retornados do processamento solicitado.
    /// </summary>
    [JsonPropertyName("data")]
    public object Dados { get; init; }
}
