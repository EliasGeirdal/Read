using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Read.ApiContracts.Request.Auth;
using Read.ApiContracts.Request.User;
using Read.ApiContracts.Response.Auth;
using Read.Application.Services.Auth;
using WebApi.Mapping;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion(1.0)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost(ApiEndpoints.Auth.Register)]
    public async Task<IActionResult> Register(UserRequest request)
    {
        var userDto = request.MapToUser();
        var user = await _authService.RegisterAsync(userDto);
        if (user is null)
        {
            return BadRequest("Username already exists.");
        }

        return Ok(user.MapToUserResponse());
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<ActionResult<TokenResponse>> Login(UserRequest request)
    {
        var userDto = request.MapToUser();
        var result = await _authService.LoginAsync(userDto);
        if (result is null)
        {
            return BadRequest("Invalid username or password.");
        }

        return Ok(result.MapToTokenResponse());
    }

    [HttpPost(ApiEndpoints.Auth.Refresh)]
    public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var refreshTokenDto = request.MapToRefreshToken();
        var result = await _authService.RefreshTokensAsync(refreshTokenDto);
        if (result is null || string.IsNullOrWhiteSpace(result.AccessToken) ||
            string.IsNullOrWhiteSpace(result.RefreshToken))
        {
            return Unauthorized("Invalid refresh token.");
        }

        return Ok(result.MapToTokenResponse());
    }
}