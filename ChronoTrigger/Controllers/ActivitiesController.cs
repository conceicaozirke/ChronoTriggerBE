using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChronoTrigger.Data;
using ChronoTrigger.Models;
using Models;

namespace ChronoTrigger.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivitiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] //retonar as atividades
        public async Task<IActionResult> GetAll()
        {
            var activities = await _context.Activities
                .Include(a => a.Company)
                .Where(a => a.IsActive)
                .Select(a => new
                {
                    a.ID,
                    a.Name,
                    a.Description,
                    a.CompanyID,
                    CompanyName = a.Company != null ? a.Company.Name : "N/A",
                    a.IsActive,
                    a.CreatedAt
                })
                .ToListAsync();

            return Ok(activities);
        }

        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateActivityDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.CompanyID <= 0)
            {
                return BadRequest(" Por favor, adicionar nome e empresa válidos à atividade.");
            }

            var activity = new Activity
            {
                Name = dto.Name,
                Description = dto.Description,
                CompanyID = dto.CompanyID,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return Ok(activity);
        }
    }

    public class CreateActivityDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CompanyID { get; set; }
    }
}