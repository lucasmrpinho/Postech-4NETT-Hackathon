using FluentValidation;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Extensions;
using PosTech.Hackathon.Pacientes.Domain.Utils;

namespace PosTech.Hackathon.Pacientes.Application.Validators;

public class PacienteValidator : AbstractValidator<CreatePacienteEvent>
{
    public PacienteValidator()
    {

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O mome é obrigatório.")
            .MinimumLength(3).WithMessage("Nome inválido.")
            .Must(name => name.IsValidName()).WithMessage("Nome com caracteres inválidos.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .Must(email => email.IsValidEmail()).WithMessage(ErrorMessageHelper.ErrorMessage[ErrorMessageHelper.PACIENTE001]);

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(cpf => cpf.IsValidCPF()).WithMessage(ErrorMessageHelper.ErrorMessage[ErrorMessageHelper.PACIENTE002]);

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Compo senha é obrigatório.");
    }
}
