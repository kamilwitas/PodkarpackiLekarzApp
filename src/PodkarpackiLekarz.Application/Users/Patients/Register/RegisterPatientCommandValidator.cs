using FluentValidation;

namespace PodkarpackiLekarz.Application.Users.Patients.Register;
public class RegisterPatientCommandValidator : AbstractValidator<RegisterPatientCommand>
{
	public RegisterPatientCommandValidator()
	{
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(x => x.PasswordConfirmation);
        RuleFor(x => x.Pesel)
            .NotEmpty()
            .Length(11, 11);
    }
}
