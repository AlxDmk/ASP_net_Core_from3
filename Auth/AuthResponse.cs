namespace Lesson3.Auth;

internal  sealed class AuthResponse
{
    public string Password { get; set; }
    public RefreshToken LatestRefreshToken { get; set; }
}