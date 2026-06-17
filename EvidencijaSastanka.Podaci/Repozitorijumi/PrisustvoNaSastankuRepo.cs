using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Podaci.Kontekst;
using Microsoft.EntityFrameworkCore;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class PrisustvoNaSastankuRepo : IPrisustvoNaSastankuRepo
    {
        private readonly AppDbContext _context;

        public PrisustvoNaSastankuRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Dodaj(PrisustvoNaSastanku prisustvo)
        {
            await _context.PrisustvoNaSastanku.AddAsync(prisustvo);
        }

        public async Task<List<PrisustvoNaSastanku>> VratiPoSastanku(int sastanakId)
        {
            return await _context.PrisustvoNaSastanku
                .Include(p => p.Stanar)
                .Where(p => p.SastanakId == sastanakId)
                .ToListAsync();
        }
    }
}