using PodkarpackiLekarz.Shared.Identity;

namespace PodkarpackiLekarz.Application.Dtos.Users;

public class AuthUserDto : IEnumTranslate
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string[] Permissions { get; set; }
    
    public void TranslateEnums()
    {
        Enum.TryParse<Role>(Role, out var roleEnum);
        Role = roleEnum.ToString();
    }
}