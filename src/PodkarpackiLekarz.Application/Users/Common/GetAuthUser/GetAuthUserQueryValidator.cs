using FluentValidation;

namespace PodkarpackiLekarz.Application.Users.Common.GetAuthUser;

public class GetAuthUserQueryValidator : AbstractValidator<GetAuthUserQuery>
{
    public GetAuthUserQueryValidator()
    {
        RuleFor(x => x.LoggedUserId)
            .NotEmpty();
    }
}