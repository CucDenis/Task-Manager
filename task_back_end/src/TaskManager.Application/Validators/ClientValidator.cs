using FluentValidation;
using TaskManager.Api.DTOs.Auth;

namespace TaskManager.Application.Validators;

public class ClientValidator: AbstractValidator<RegisterDto>
{
    public ClientValidator(){
        RuleFor( x => x.Name)
        .NotEmpty()
        .MaximumLength(255);

        RuleFor( x => x.CuiCnp)
        .NotEmpty()
        .MaximumLength(16);

        RuleFor( x => x.Email)
        .NotEmpty()
        .EmailAddress()
        .MaximumLength(255);

        RuleFor( x => x.Password)
        .NotEmpty()
        .MaximumLength(255);
    }
}