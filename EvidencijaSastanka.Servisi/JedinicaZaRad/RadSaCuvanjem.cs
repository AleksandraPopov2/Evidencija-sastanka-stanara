using EvidencijaSastanka.Podaci.Kontekst;
using EvidencijaSastanka.Servisi.Interfejsi;

namespace EvidencijaSastanka.Servisi.JedinicaZaRad
{
    public class RadSaCuvanjem(AppDbContext context) : IRadSaCuvanjem
    {
        private readonly AppDbContext _context = context;

        public Task SacuvajPromene()
        {
            return _context.SaveChangesAsync();
        }
    }
}
