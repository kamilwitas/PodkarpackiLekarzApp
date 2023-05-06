using FluentValidation;

namespace PodkarpackiLekarz.Application.Users.Administrators.AddAdministrator;
public class AddAdministratorCommandValidator : AbstractValidator<AddAdministratorCommand>
{
    public AddAdministratorCommandValidator()
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
    }
}
