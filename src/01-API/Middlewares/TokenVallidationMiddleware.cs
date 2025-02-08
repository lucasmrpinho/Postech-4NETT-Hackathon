using PosTech.Hackathon.Pacientes.Application.Requests;

namespace PosTech.Hackathon.Pacientes.API.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpClient _httpClient;

    public TokenValidationMiddleware(RequestDelegate next, HttpClient httpClient)
    {
        _next = next;
        _httpClient = httpClient;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method != "POST")
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token não informado.");
                return;
            }

            var resultado = await ValidarToken(token);

            if (!resultado)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Acesso negado: Token inválido.");
                return;
            }
        }

        await _next(context);
    }

    private async Task<bool> ValidarToken(string token)
    {
        var urlValidacao = "http://service-hackathon-autenticacao/v1/autenticacao/validacoes-tokens"; // Ajuste a URL correta

        var response = await _httpClient.PostAsJsonAsync(urlValidacao, new { token = token });

        if (!response.IsSuccessStatusCode)
            return false;

        var resultado = await response.Content.ReadFromJsonAsync<SaidaPadrao>();
        return resultado?.Sucesso ?? false;
    }
}
