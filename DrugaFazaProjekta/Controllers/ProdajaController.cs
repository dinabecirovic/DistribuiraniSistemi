using Microsoft.AspNetCore.Mvc;
using DrugaFazaProjekta.Models;

namespace DrugaFazaProjekta.Controllers
{
    [ApiController]
    [Route("api/prodaja")]
    public class ProdajaController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProdajaController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Prodaja.ToList());
        }

        [HttpPost]
        public IActionResult Add(Prodaja prodaja)
        {
            _context.Prodaja.Add(prodaja);
            _context.SaveChanges();
            return Ok(prodaja);
        }
    }
}
