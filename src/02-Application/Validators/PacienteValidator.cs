using FluentValidation;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Extensions;
using static PosTech.Hackathon.Pacientes.Domain.Utils.ErrorMessageHelper;

namespace PosTech.Hackathon.Pacientes.Application.Validators;

public class PacienteValidator : AbstractValidator<CreatePacienteEvent>
{
    public PacienteValidator()
    {

        RuleFor(x => new { Email = x.EmailPaciente })
             .Custom((value, context) =>
             {
                 if (!value.Email.IsValidEmail())
                     context.AddFailure(PACIENTE001);
             });

        RuleFor(x => new { CPF = x.CPFPaciente })
             .Custom((value, context) =>
             {
                 if (!value.CPF.IsValidCPF())
                     context.AddFailure(PACIENTE002);
             });

    }
}
