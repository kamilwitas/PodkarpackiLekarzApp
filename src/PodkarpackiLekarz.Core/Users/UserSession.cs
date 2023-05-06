namespace PodkarpackiLekarz.Core.Users;
public class UserSession
{
    public Guid Id { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiryDate { get; private set; }
    public bool Revoked { get; private set; }
    public IdentityUser IdentityUser { get; private set; }

    public UserSession() { }

    public UserSession(Guid id)
    {
        Id = id;
    }

    private UserSession(
        Guid id,
        string refreshToken,
        DateTime refreshTokenExpiryDate)
    {
        Id = id;
        RefreshToken = refreshToken;
        RefreshTokenExpiryDate = refreshTokenExpiryDate;
        Revoked = false;
    }

    internal static UserSession SetSession(
        string refreshToken,
        DateTime refreshTokenExpirationDate)
        => new UserSession(
            Guid.NewGuid(),
            refreshToken,
            refreshTokenExpirationDate);

    internal void Revoke()
    {
        Revoked = true;
    }
}
