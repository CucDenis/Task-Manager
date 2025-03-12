namespace TaskManager.Domain.Interfaces;

public interface IJwtUser
{
    int Id { get; }
    string? Email { get; }
    string? FirstName { get; }
    string? LastName { get; }
}
