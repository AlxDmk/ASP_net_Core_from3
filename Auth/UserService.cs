using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lesson3.Auth;

internal sealed class UserService:IUserService
{
    private readonly IDictionary<string, AuthResponse> _users = new Dictionary<string, AuthResponse>()
    {
        {"test", new AuthResponse() {Password = "test"}}
    };

    public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!";
    
    
    public TokenResponse Authenticate(string user, string password)
    {
        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        TokenResponse tokenResponse = new TokenResponse();
        int i = 0;
        foreach (KeyValuePair<string, AuthResponse> pair in _users)
        {
            i++;
            if (string.CompareOrdinal(pair.Key, user) == 0 && string.CompareOrdinal(pair.Value.Password, password) == 0)
            {
                tokenResponse.Token = GenerateJwtToken(i, 15);
                RefreshToken refreshToken = GenerateRefreshToken(i);
                pair.Value.LatestRefreshToken = refreshToken;
                tokenResponse.RefreshToken = refreshToken.Token;

                return tokenResponse;
            }
        }

        return null;

    }

    private RefreshToken GenerateRefreshToken(int id)
    {
        RefreshToken refreshToken = new RefreshToken();
        refreshToken.Expires = DateTime.UtcNow.AddMinutes(360);
        refreshToken.Token = GenerateJwtToken(id, 360);
        return refreshToken;
    }

    private string GenerateJwtToken(int id, int time)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(SecretCode);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(time),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string RefreshToken(string token)
    {
        int i = 0;
        foreach (KeyValuePair<string, AuthResponse> pair in _users)
        {
            i++;
            if (string.CompareOrdinal(pair.Value.LatestRefreshToken.Token, token) == 0 && pair.Value.LatestRefreshToken.IsExpired is false)
            {
                pair.Value.LatestRefreshToken = GenerateRefreshToken(i);
                return pair.Value.LatestRefreshToken.Token;
            }
        }

        return string.Empty;
    }
}