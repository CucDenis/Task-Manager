using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Interfaces;
using FluentValidation;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtService _jwtService;
    private readonly IValidator<RegisterDto> _validator;

    public UsersController(IUnitOfWork unitOfWork, JwtService jwtService, IValidator<RegisterDto> validator)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _validator = validator;
    }

}
