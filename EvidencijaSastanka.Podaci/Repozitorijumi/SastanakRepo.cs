using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Podaci.Kontekst;
using Microsoft.EntityFrameworkCore;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class SastanakRepo : ISastanakRepo
    {
        private readonly AppDbContext _context;

        public SastanakRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sastanak>> Vratisve()
        {
            return await _context.Sastanak
                .Include(s => s.Zgrada)
                .ToListAsync();
        }

        public async Task<Sastanak?> VratiPoIdu(int id)
        {
            return await _context.Sastanak
                .Include(s => s.Zgrada)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sastanak> Dodaj(Sastanak sastanak)
        {
            await _context.Sastanak.AddAsync(sastanak);
            return sastanak;
        }

        public async Task<Sastanak?> Izmeni(int id, Sastanak izmenjenSastanak)
        {
            var stariSastanak = await _context.Sastanak
                .FirstOrDefaultAsync(s => s.Id == id);

            if (stariSastanak is null)
                return null;

            stariSastanak.Tema = izmenjenSastanak.Tema;
            stariSastanak.DatumSastanka = izmenjenSastanak.DatumSastanka;
            stariSastanak.BrojPrisutnih = izmenjenSastanak.BrojPrisutnih;
            stariSastanak.ProcenatPrisutnih = izmenjenSastanak.ProcenatPrisutnih;
            stariSastanak.StatusSastanka = izmenjenSastanak.StatusSastanka;
            stariSastanak.Zakljucak = izmenjenSastanak.Zakljucak;
            stariSastanak.ZgradaId = izmenjenSastanak.ZgradaId;

            return stariSastanak;
        }

        public async Task<bool> Obrisi(int id)
        {
            var sastanak = await _context.Sastanak
                .Include(s => s.Prisustva)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sastanak is null)
                return false;

            _context.PrisustvoNaSastanku.RemoveRange(sastanak.Prisustva);

            _context.Sastanak.Remove(sastanak);

            return true;
        }

        public async Task<List<Sastanak>> VratiPoZgradi(int zgradaId)
        {
            return await _context.Sastanak
                .Include(s => s.Zgrada)
                .Where(s => s.ZgradaId == zgradaId)
                .ToListAsync();
        }

        public async Task<Sastanak?> VratiZaZapisnik(int id)
        {
            return await _context.Sastanak
                .Include(s => s.Zgrada)
                .Include(s => s.Prisustva)
                    .ThenInclude(p => p.Stanar)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}