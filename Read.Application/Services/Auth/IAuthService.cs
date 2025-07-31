using Read.Application.Models;
using Read.Application.Models.Dto;

namespace Read.Application.Services.Auth;

public interface IAuthService
{
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenDto?> LoginAsync(UserDto request);
    Task<TokenDto?> RefreshTokensAsync(RefreshTokenDto request);
}