using FluentValidation;

namespace PodkarpackiLekarz.Application.Users.Doctors.Register;
public class RegisterDoctorCommandValidator : AbstractValidator<RegisterDoctorCommand>
{
	public RegisterDoctorCommandValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(x => x.PasswordConfirmation);
        RuleFor(x => x.DoctorTypeId)
            .NotEmpty();
        RuleFor(x => x.MedicalLicenseNumber)
            .NotEmpty();
    }
}
