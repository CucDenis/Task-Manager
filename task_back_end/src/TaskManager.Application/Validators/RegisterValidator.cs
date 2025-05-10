using FluentValidation;
using TaskManager.Application.DTOs.Auth;

namespace TaskManager.Application.Validators;

public class RegisterValidator: AbstractValidator<RegisterDto>
{
    public RegisterValidator(){
        RuleFor( x => x.FirstName)
        .NotEmpty()
        .MaximumLength(255);

        RuleFor( x => x.LastName)
        .NotEmpty()
        .MaximumLength(255);

        RuleFor( x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .MaximumLength(255);

        RuleFor( x => x.Cnp)
        .NotEmpty()
        .MaximumLength(16);

        RuleFor( x => x.Password)
        .NotEmpty()
        .MaximumLength(255);

        RuleFor( x => x.RoleId)
        .NotEmpty();
    }
}