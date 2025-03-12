using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Models;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Features.Interventions.Queries;

public class GetInterventionsQueryHandler : IRequestHandler<GetInterventionsQuery, PagedResponse<InterventionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInterventionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResponse<InterventionDto>> Handle(
        GetInterventionsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<InterventionDocument>()
            .GetQueryable()
            .Include(i => i.Technician)
            .Include(i => i.Client)
            .AsQueryable();


        if (!string.IsNullOrWhiteSpace(request.InterventionName))
        {
            query = query.Where(i => i.Name != null && i.Name.Contains(request.InterventionName));
        }

        if (!string.IsNullOrWhiteSpace(request.TechnicianName))
        {
            query = query.Where(i => i.Technician != null && 
                (i.Technician.FirstName + " " + i.Technician.LastName)
                .Contains(request.TechnicianName));
        }

        if (!string.IsNullOrWhiteSpace(request.ClientName))
        {
            query = query.Where(i => i.Client != null && 
                (i.Client.Name ?? string.Empty).Contains(request.ClientName));
        }

        if (!string.IsNullOrWhiteSpace(request.InterventionDate))
        {
            try
            {
                var dateString = request.InterventionDate.Trim('"');
                var parsedDate = DateTime.Parse(dateString).ToUniversalTime();
                var startOfDay = DateTime.SpecifyKind(parsedDate.Date, DateTimeKind.Utc);
                
                query = query.Where(i => i.InterventionDate.Date == startOfDay.Date);
            }
            catch
            {
                // If date parsing fails, just skip the date filter
            }
        }


        var totalCount = await query.CountAsync(cancellationToken);


        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new InterventionDto
            {
                Id = i.Id,
                WorkPointAddress = i.WorkPointAddress ?? string.Empty,
                ContactPerson = i.ContactPerson ?? string.Empty,
                InterventionDate = i.InterventionDate.ToString(),
                Status = i.Status ?? string.Empty,
                TimeInterval = i.TimeInterval ?? string.Empty,
                TechnicianName = i.Technician != null 
                    ? $"{i.Technician.FirstName} {i.Technician.LastName}"
                    : string.Empty,
                ClientName = i.Client != null ? i.Client.Name ?? string.Empty : string.Empty
            })
            .ToListAsync(cancellationToken);

        return new PagedResponse<InterventionDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
