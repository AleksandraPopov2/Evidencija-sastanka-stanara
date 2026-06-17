using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Podaci.Kontekst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class StanarRepo : IStanarRepo
    {
        private readonly AppDbContext _context;
        public StanarRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Stanar> Dodaj(Stanar stanar)
        {
            await _context.AddAsync(stanar);
            return stanar;
        }

        public async Task<Stanar?> Izmeni(int id, Stanar izmenjenStanar)
        {
            var stariStanar = await _context.Stanar.FirstOrDefaultAsync(x => x.Id == id);
            if(stariStanar is null)
            {
                return null;
            }
            stariStanar.Ime = izmenjenStanar.Ime;
            stariStanar.Prezime = izmenjenStanar.Prezime;
            stariStanar.ZgradaId = izmenjenStanar.ZgradaId;
            stariStanar.Email = izmenjenStanar.Email;
            stariStanar.BrojStana = izmenjenStanar.BrojStana;
            return stariStanar;
        }

        public async Task<bool> Obrisi(int id)
        {
            var stanar = await _context.Stanar
                .FirstOrDefaultAsync(x => x.Id == id);

            if (stanar is null)
                return false;

            var prisustva = await _context.PrisustvoNaSastanku
                .Where(p => p.StanarId == id)
                .ToListAsync();

            _context.PrisustvoNaSastanku.RemoveRange(prisustva);

            _context.Stanar.Remove(stanar);

            return true;
        }

        public async Task<Stanar?> VratiPoIdu(int id)
        {
            var stanar = await _context.Stanar.FirstOrDefaultAsync(x => x.Id == id);
            if (stanar is null)
            {
                return null;
            }
            return stanar;
        }

        public async Task<List<Stanar>> Vratisve()
        {
            return await _context.Stanar
       .Include(s => s.Zgrada)
       .ToListAsync();
        }
        public async Task<List<Stanar>> VratiPoZgradi(int zgradaId)
        {
            return await _context.Stanar
                .Include(s => s.Zgrada)
                .Where(s => s.ZgradaId == zgradaId)
                .ToListAsync();
        }
        public async Task<int> PrebrojPoZgradi(int zgradaId)
        {
            return await _context.Stanar
                .CountAsync(s => s.ZgradaId == zgradaId);
        }
    }
}
