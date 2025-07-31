namespace Read.ApiContracts.Request.Auth;

public class RefreshTokenRequest
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}