using ChronoTrigger.Data; 
using ChronoTrigger.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ChronoTrigger.API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] //buscar nomes das empresas
        public async Task<IActionResult> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            return Ok(companies);
        }

        [HttpPost] //adc nome das empresas
        public async Task<IActionResult> Create([FromBody] Company company)
        {
            company.CreatedAt = DateTime.UtcNow;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return Ok(company);
        }
    }
}