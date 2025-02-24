﻿using System.ComponentModel.DataAnnotations;

namespace PosTech.GrupoOito.Hackathon.PacienteManagement.Events;

public class CreatePacienteEvent
{
    /// <summary>
    /// Nome do Paciente.
    /// </summary>
    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// CPF do Paciente. 
    /// </summary>
    [Required(ErrorMessage = "CPF é obrigatório.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$", ErrorMessage = "CPF deve estar no formato 000.000.000-00 ou 00000000000.")]
    public string Cpf { get; set; } = string.Empty;

    /// <summary>
    /// Endereço de E-mail do Paciente.
    /// </summary>
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [MaxLength(100, ErrorMessage = "E-mail deve conter no máximo 60 caracteres.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Senha do Paciente. 
    /// </summary>
    [Required(ErrorMessage = "Senha é obrigatória.")]
    [MaxLength(100, ErrorMessage = "A senha deve deve conter no máximo 100 caracteres.")]
    public string Senha { get; set; } = string.Empty;

}
