using EvidencijaSastanka.DTO.StanarDTO;
using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Servisi.Interfejsi;
using EvidencijaSastanka.Servisi.JedinicaZaRad;

namespace EvidencijaSastanka.Servisi.Servisi
{
    public class StanarServis : IStanarServis
    {
        private readonly IStanarRepo _stanarRepo;
        private readonly IRadSaCuvanjem _radSaCuvanjem;

        public StanarServis(IStanarRepo stanarRepo, IRadSaCuvanjem radSaCuvanjem)
        {
            _stanarRepo = stanarRepo;
            _radSaCuvanjem = radSaCuvanjem;
        }

        public async Task<List<PrikazStanaraDTO>> Vratisve()
        {
            var stanari = await _stanarRepo.Vratisve();

            return stanari.Select(s => new PrikazStanaraDTO
            {
                Id = s.Id,
                Ime = s.Ime,
                Prezime = s.Prezime,
                Email = s.Email,
                BrojStana = s.BrojStana,
                ZgradaId = s.ZgradaId,
                NazivZgrade = s.Zgrada != null ? s.Zgrada.Naziv : string.Empty
            }).ToList();
        }

        public async Task<PrikazStanaraDTO?> VratiPoIdu(int id)
        {
            var stanar = await _stanarRepo.VratiPoIdu(id);

            if (stanar is null)
                return null;

            return new PrikazStanaraDTO
            {
                Id = stanar.Id,
                Ime = stanar.Ime,
                Prezime = stanar.Prezime,
                Email = stanar.Email,
                BrojStana = stanar.BrojStana,
                ZgradaId = stanar.ZgradaId,
                NazivZgrade = stanar.Zgrada != null ? stanar.Zgrada.Naziv : string.Empty
            };
        }

        public async Task<PrikazStanaraDTO> Dodaj(StanarDodavanjeDTO dto)
        {
            var stanar = new Stanar
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Email = dto.Email,
                BrojStana = dto.BrojStana,
                ZgradaId = dto.ZgradaId
            };

            await _stanarRepo.Dodaj(stanar);
            await _radSaCuvanjem.SacuvajPromene();

            return new PrikazStanaraDTO
            {
                Id = stanar.Id,
                Ime = stanar.Ime,
                Prezime = stanar.Prezime,
                Email = stanar.Email,
                BrojStana = stanar.BrojStana,
                ZgradaId = stanar.ZgradaId,
                NazivZgrade = string.Empty
            };
        }

        public async Task<PrikazStanaraDTO?> Izmeni(int id, PrikazStanaraDTO dto)
        {
            var izmenjenStanar = new Stanar
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Email = dto.Email,
                BrojStana = dto.BrojStana,
                ZgradaId = dto.ZgradaId
            };

            var stanar = await _stanarRepo.Izmeni(id, izmenjenStanar);

            if (stanar is null)
                return null;

            await _radSaCuvanjem.SacuvajPromene();

            return new PrikazStanaraDTO
            {
                Id = stanar.Id,
                Ime = stanar.Ime,
                Prezime = stanar.Prezime,
                Email = stanar.Email,
                BrojStana = stanar.BrojStana,
                ZgradaId = stanar.ZgradaId,
                NazivZgrade = stanar.Zgrada != null ? stanar.Zgrada.Naziv : string.Empty
            };
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisan = await _stanarRepo.Obrisi(id);

            if (!obrisan)
                return false;

            await _radSaCuvanjem.SacuvajPromene();

            return true;
        }
        public async Task<List<PrikazStanaraDTO>> VratiPoZgradi(int zgradaId)
        {
            var stanari = await _stanarRepo.VratiPoZgradi(zgradaId);

            return stanari.Select(s => new PrikazStanaraDTO
            {
                Id = s.Id,
                Ime = s.Ime,
                Prezime = s.Prezime,
                Email = s.Email ?? string.Empty,
                BrojStana = s.BrojStana,
                ZgradaId = s.ZgradaId,
                NazivZgrade = s.Zgrada != null ? s.Zgrada.Naziv : string.Empty
            }).ToList();
        }
    }
}