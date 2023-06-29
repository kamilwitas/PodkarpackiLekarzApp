namespace PodkarpackiLekarz.Application.Auth
{
    public interface ICookieAuthorizationService
    {
        Task SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}