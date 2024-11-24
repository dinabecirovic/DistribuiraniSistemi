using Microsoft.AspNetCore.Mvc;
using DrugaFazaProjekta.Models;

namespace DrugaFazaProjekta.Controllers
{
    [ApiController]
    [Route("api/proizvodi")]
    public class ProizvodiController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProizvodiController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Proizvodi.ToList());
        }

        [HttpPost]
        public IActionResult Add(Proizvod proizvod)
        {
            _context.Proizvodi.Add(proizvod);
            _context.SaveChanges();
            return Ok(proizvod);
        }
    }
}
