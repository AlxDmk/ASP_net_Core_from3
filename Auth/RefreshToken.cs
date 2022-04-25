using System;

namespace Lesson3.Auth;

public sealed class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    
}