using System.Globalization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Abstractions.Repositories;
using TaskManager.Application.Common.Models;
using TaskManager.Application.DTOs.Intervention;
using TaskManager.Application.Features.Interventions.Queries.GetInterventions;

namespace TaskManager.Application.Features.Interventions.Queries;

public class GetInterventionsQueryHandler(IInterventionsRepository interventionsRepository) : IRequestHandler<GetInterventionsQuery, PagedResponse<InterventionDto>>
{
    private readonly IInterventionsRepository _interventionsRepository = interventionsRepository;

    public async Task<PagedResponse<InterventionDto>> Handle(
        GetInterventionsQuery request, CancellationToken cancellationToken)
    {
    
        var queryInterventions = _interventionsRepository
            .GetQueryable()
            .Include(i => i.Client)
                .ThenInclude(c => c!.User)
            .Include(i => i.Technician)
                .ThenInclude(t => t!.User)
            .Include(i => i.Level)
            .Select(i => new
            {
                i.Id,
                Name = i.Name ?? string.Empty,
                ClientName = i.Client != null && i.Client.User != null ? i.Client.User.FirstName + " " + i.Client.User.LastName : string.Empty,
                InterventionDate = i.CreatedAt,
                TechnicianName = i.Technician != null && i.Technician.User != null ? i.Technician.User.FirstName + " " + i.Technician.User.LastName : string.Empty,
                UrgencyLevel = i.Level != null ? i.Level.Name : string.Empty
            });

        if (!string.IsNullOrWhiteSpace(request.InterventionName))
        {
            queryInterventions = queryInterventions.Where(i => i.Name != null && i.Name.Contains(request.InterventionName));
        }

        if (!string.IsNullOrWhiteSpace(request.TechnicianName))
        {
            queryInterventions = queryInterventions.Where(i => i.TechnicianName != null && i.TechnicianName.Contains(request.TechnicianName));
        }

        if (!string.IsNullOrWhiteSpace(request.ClientName))
        {
            queryInterventions = queryInterventions.Where(i => i.ClientName != null && i.ClientName.Contains(request.ClientName));
        }

        if (!string.IsNullOrWhiteSpace(request.InterventionDate))
        {
            try
            {
                string dateString = request.InterventionDate.Trim('"');
                var parsedDate = DateTime.Parse(dateString, new CultureInfo("en-GB"));
                var startOfDay = DateTime.SpecifyKind(parsedDate.Date, DateTimeKind.Utc);
                
                queryInterventions = queryInterventions.Where(i => i.InterventionDate == startOfDay.Date);
            }
            catch
            {
                // If date parsing fails, just skip the date filter
            }
        }


        int totalCount = await queryInterventions.CountAsync(cancellationToken);


        List<InterventionDto> items = await queryInterventions
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new InterventionDto
            {
                Id = i.Id,
                InterventionDate = i.InterventionDate.ToString(),
                UrgencyLevel = i.UrgencyLevel ?? string.Empty,
                TechnicianName = i.TechnicianName,
                ClientName = i.ClientName
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
