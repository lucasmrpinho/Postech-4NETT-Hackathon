using PosTech.Hackathon.Pacientes.Application.Requests;
using PosTech.Hackathon.Pacientes.Application.Responses;

namespace PosTech.Hackathon.Pacientes.Application.Services.Interfaces;

public interface IAutenticacaoClient
{
    Task<UsuarioResponse> SaveAsync(UsuarioRequest request);
    Task<bool> VerifyToken(string token);
    Task<TokenRequest> GetServiceToken(TokenServicoRequest request);
}
