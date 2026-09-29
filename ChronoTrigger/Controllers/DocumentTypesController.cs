using ChronoTrigger.Data;
using ChronoTrigger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChronoTrigger.API.Controllers
{
    [ApiController]
    [Route("api/documenttypes")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentTypesController(AppDbContext context)
        {
            _context = context;
        }
         
        [HttpGet] //trocar método ---- post para adc novo tipo de documento / get para retornar a lista ----
        public async Task<IActionResult> GetAll()
        {
            var docTypes = new[]
            {
                new { Id = "CNPJ", Description = "Cadastro Nacional da Pessoa Jurídica" },
                new { Id = "CPF", Description = "Cadastro de Pessoas Físicas" }
            };

            return Ok(docTypes);
        }
    }
}