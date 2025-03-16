namespace TaskManager.Api.DTOs.Auth;

public class RegisterDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CuiCnp { get; set; } = string.Empty;
    public ICollection<ClientContactDto> Contacts { get; set; } = [];
}

public class ClientContactDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
