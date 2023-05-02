namespace PodkarpackiLekarz.Shared.Exceptions;
public abstract class AuthorizationException : Exception
{
	public AuthorizationException(string message)
		:base(message)
	{

	}
}
