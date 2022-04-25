namespace Lesson3.Auth;

public interface IUserService
{
    TokenResponse Authenticate(string user, string password);

    string RefreshToken(string token);
}