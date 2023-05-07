using FluentValidation;

namespace PodkarpackiLekarz.Application.Users.Doctors.AddDoctorType;

public class AddDoctorTypeCommandValidator : AbstractValidator<AddDoctorTypeCommand>
{
    public AddDoctorTypeCommandValidator()
    {
        RuleFor(x => x.Speciality)
            .NotEmpty()
            .MaximumLength(30);
    }
}