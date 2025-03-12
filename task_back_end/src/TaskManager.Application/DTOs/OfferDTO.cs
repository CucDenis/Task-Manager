using System;

namespace TaskManager.Application.DTOs;

public class OfferDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateOnly? ValidityPeriod { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? ClientCompany { get; set; }
}
