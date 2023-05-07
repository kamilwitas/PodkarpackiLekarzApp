using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Application.Dtos.Users;
public class IdentityUserDto : IEnumTranslate
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public void TranslateEnums()
    {
        Enum.TryParse<Role>(Role, true, out Role role);
        Role = role.ToString();
    }
}
