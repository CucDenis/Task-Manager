using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Validators;

namespace TaskManager.Application;

public static class DependecyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services){

        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        return services;
    }
}
