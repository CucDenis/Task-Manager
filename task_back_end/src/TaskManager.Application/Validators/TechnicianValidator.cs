using FluentValidation;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Validators;

public class TechnicianValidator : AbstractValidator<Technician>
{
    public TechnicianValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(255);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(255);
            
    }
}
