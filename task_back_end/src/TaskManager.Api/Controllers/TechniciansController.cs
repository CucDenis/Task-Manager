using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TechniciansController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TechniciansController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TechnicianDTO>>> GetAll()
    {
        try
        {
            var technicians = await _unitOfWork.Repository<Technician>().GetAllAsync();
            var technicianDTOs = technicians.Select(t => new TechnicianDTO
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Signature = t.Signature,
                Status = t.Status
            });
            
            return Ok(technicianDTOs);
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving technicians" });
        }
    }

}
