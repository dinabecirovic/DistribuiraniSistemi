using DrugaFazaProjekta.Models;
using Microsoft.EntityFrameworkCore;

namespace DrugaFazaProjekta.Services
{
    public class PriceOptimizationService
    {
        private readonly DatabaseContext _context;

        public PriceOptimizationService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task OptimizujCene()
        {
            var danas = DateTime.Today;

            var dnevneProdaje = await _context.Prodaja
                .Where(prodaja => prodaja.DatumVreme.Date == danas)
                .GroupBy(prodaja => prodaja.IdProizvoda)
                .Select(grupa => new
                {
                    IdProizvoda = grupa.Key,
                    UkupnaKolicina = grupa.Sum(prodaja => prodaja.Kolicina)
                })
                .ToListAsync();

            foreach (var prodaja in dnevneProdaje)
            {
                var proizvod = await _context.Proizvodi.FindAsync(prodaja.IdProizvoda);

                if (proizvod != null)
                {
                    if (prodaja.UkupnaKolicina > 30)
                    {
                        proizvod.Cena *= 1.20f;
                    }
                    else if (prodaja.UkupnaKolicina > 10)
                    {
                        proizvod.Cena *= 1.15f;
                    }

                    _context.Proizvodi.Update(proizvod);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
