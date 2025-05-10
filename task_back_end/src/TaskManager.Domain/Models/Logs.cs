
namespace TaskManager.Domain.Models;

public class Logs
{
    public Guid Id { get; set; }

    public required string ErrorCode { get; set; }
}
