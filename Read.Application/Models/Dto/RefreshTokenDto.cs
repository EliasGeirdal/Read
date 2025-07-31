namespace Read.Application.Models.Dto;

public class RefreshTokenDto
{
    public Guid UserId { get; set; }
    public required string RefreshToken { get; set; }
}