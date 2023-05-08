namespace PodkarpackiLekarz.Api.Requests.Users;

public record RefreshAccessTokenRequest(string ExpiredAccessToken, string RefreshToken);