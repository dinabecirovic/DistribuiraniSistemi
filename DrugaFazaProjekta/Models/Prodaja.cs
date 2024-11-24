namespace DrugaFazaProjekta.Models
{
    public class Prodaja
    {
        public int Id { get; set; }
        public int IdProizvoda { get; set; }
        public DateTime DatumVreme { get; set; }
        public int Kolicina { get; set; }

        public Proizvod Proizvod { get; set; }
    }
}
