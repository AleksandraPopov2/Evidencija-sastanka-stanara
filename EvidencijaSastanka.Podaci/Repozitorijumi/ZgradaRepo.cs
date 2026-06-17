using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Podaci.Kontekst;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class ZgradaRepo(AppDbContext context) : IZgradaRepo
    {
        private readonly AppDbContext _context = context;

        public async Task<Zgrada> Dodaj(Zgrada zgrada)
        {
            await _context.Zgrada.AddAsync(zgrada);
            return zgrada;
        }

        public async Task<Zgrada?> Izmeni(int id, Zgrada izmenjenaZgrada)
        {
            var staraZgrada = await _context.Zgrada.FirstOrDefaultAsync(x => x.Id == id);
            if (staraZgrada is null)
            {
                return null;
            }
            staraZgrada.Naziv = izmenjenaZgrada.Naziv;
            staraZgrada.Adresa = izmenjenaZgrada.Adresa;
            return staraZgrada;
        }

        public async Task<bool> Obrisi(int id)
        {
            var zgrada = await _context.Zgrada
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zgrada is null)
                return false;

            var sastanci = await _context.Sastanak
                .Where(s => s.ZgradaId == id)
                .ToListAsync();

            foreach (var sastanak in sastanci)
            {
                var prisustva = await _context.PrisustvoNaSastanku
                    .Where(p => p.SastanakId == sastanak.Id)
                    .ToListAsync();

                _context.PrisustvoNaSastanku.RemoveRange(prisustva);
            }

            _context.Sastanak.RemoveRange(sastanci);

            var stanari = await _context.Stanar
                .Where(s => s.ZgradaId == id)
                .ToListAsync();

            _context.Stanar.RemoveRange(stanari);

            _context.Zgrada.Remove(zgrada);

            return true;
        }

        public async Task<Zgrada?> VratiPoIdu(int id)
        {
            var zgrada = await _context.Zgrada.FirstOrDefaultAsync(x => x.Id == id);
            if(zgrada is null)
            {
                return null;
            }
            return zgrada;
        }

        public async Task<List<Zgrada>> Vratisve()
        {
            return await _context.Zgrada.ToListAsync();
        }


    }
}