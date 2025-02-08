using System.ComponentModel.DataAnnotations;

namespace PosTech.Hackathon.Pacientes.Application.Requests;

public enum TipoPerfilEnumerador
{
    Medico,
    Paciente
}
public class UsuarioRequest
{
    public UsuarioRequest(string nome, string email, string documento, string senha)
    {
        Nome = nome;
        Email = email;
        Documento = documento;
        Senha = senha;
        TipoPerfil = TipoPerfilEnumerador.Paciente;
    }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    /// <summary>
    /// Email do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    public string Email { get; set; }

    /// <summary>
    /// Documento do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo Documento é obrigatório.")]
    public string Documento { get; set; }

    /// <summary>
    /// Senha do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string Senha { get; set; }

    /// <summary>
    /// Tipo de perfil do usuário.
    /// </summary>
    [Required(ErrorMessage = "O campo TipoPerfil é obrigatório.")]
    public TipoPerfilEnumerador TipoPerfil { get; set; }
}
