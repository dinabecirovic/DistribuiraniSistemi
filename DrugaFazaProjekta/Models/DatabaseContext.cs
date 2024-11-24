using Microsoft.EntityFrameworkCore;

namespace DrugaFazaProjekta.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Prodaja> Prodaja { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
