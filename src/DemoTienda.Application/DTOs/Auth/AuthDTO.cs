namespace DemoTienda.Application.DTOs.Auth;

public class AuthDTO
{
    public record RegisterRequest(string UserName, string Password, string FullName);
    public record LoginRequest(string UserName, string Password);
    public record AuthResponse(string AccessToken);
}