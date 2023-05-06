using PodkarpackiLekarz.Core.Users.Exceptions;
using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Core.Users.Admins;
public class Administrator : IdentityUser
{
    private Administrator(
        Guid id,
        string firstName,
        string lastName,
        string email) : base(id, firstName, lastName, email, Role.Admin)
    {
    }

    public Administrator()
    {

    }

    internal static Administrator Create(
        string firstName,
        string lastName,
        string email)
    {
        ValidateInput(
            firstName,
            lastName,
            email);

        return new Administrator(Guid.NewGuid(),firstName, lastName, email);
    }

    internal static Administrator CreateInitialAdmin(
        Guid id,
        string firstName,
        string lastName,
        string email)
    {
        ValidateInput(
            firstName,
            lastName,
            email);

        return new Administrator(id, firstName, lastName, email);
    }

    private static void ValidateInput(
        string firstName,
        string lastName,
        string email)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new FieldCannotBeNullOrEmptyException(nameof(firstName));

        if (string.IsNullOrEmpty(lastName))
            throw new FieldCannotBeNullOrEmptyException(nameof(lastName));

        if (string.IsNullOrEmpty(email))
            throw new FieldCannotBeNullOrEmptyException(nameof(email));
    }

}
