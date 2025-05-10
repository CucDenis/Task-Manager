namespace TaskManager.Application.DTOs.Auth;

public class RegisterDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Cnp { get; set; }
    public required string Password { get; set; }
    public required Guid RoleId { get; set; }

}

