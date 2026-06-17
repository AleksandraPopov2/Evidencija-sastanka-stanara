using EvidencijaSastanka.DTO.KorisnikDTO;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Servisi.Interfejsi;

namespace EvidencijaSastanka.Servisi.Servisi
{
    public class KorisnikServis : IKorisnikServis
    {
        private readonly IKorisnikRepo _korisnikRepo;

        public KorisnikServis(IKorisnikRepo korisnikRepo)
        {
            _korisnikRepo = korisnikRepo;
        }

        public async Task<PrikazKorisnikaDTO?> Login(PrijavaDTO dto)
        {
            var korisnik = await _korisnikRepo.Login(dto.KorisnickoIme, dto.Lozinka);

            if (korisnik is null)
                return null;

            return new PrikazKorisnikaDTO
            {
                Id = korisnik.Id,
                KorisnickoIme = korisnik.KorisnickoIme,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime
            };
        }
    }
}