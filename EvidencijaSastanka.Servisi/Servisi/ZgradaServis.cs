using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Servisi.Interfejsi;
using EvidencijaSastanka.Servisi.DTO.ZgradaDTO;
using EvidencijaSastanka.DTO.ZgradaDTO;

namespace EvidencijaSastanka.Servisi.DTO.ZgradaDTO

{
    public class ZgradaServis(IZgradaRepo repo, IRadSaCuvanjem cuvanje) : IZgradaServis
    {
        private readonly IZgradaRepo _repo = repo;
        private readonly IRadSaCuvanjem _cuvanje = cuvanje;

        public async Task<ZgradaDodavanjeDTO> Dodaj(ZgradaDodavanjeDTO dto)
        {
            var zgrada = new Zgrada()
            {
                Naziv = dto.Naziv,
                Adresa = dto.Adresa,
                
            };
            await _repo.Dodaj(zgrada);
            await _cuvanje.SacuvajPromene();
            return new ZgradaDodavanjeDTO
            {
                
                Naziv = zgrada.Naziv,
                Adresa = zgrada.Adresa,
                
            };
        }

        public async Task<PrikazZgradeDTO?> Izmeni(int id, PrikazZgradeDTO dto)
        {
            var izmenjenaZgrada = new Zgrada
            {
                Id = dto.Id,
                Naziv = dto.Naziv,
                Adresa = dto.Adresa,
                
            };
            var zgrada = await _repo.Izmeni(id,izmenjenaZgrada);
            if(zgrada is null)
            {
                return null;
            }
            await _cuvanje.SacuvajPromene();
            return new PrikazZgradeDTO
            {
                Id = zgrada.Id,
                Naziv = zgrada.Naziv,
                Adresa = zgrada.Adresa,
                

            };
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisana = await _repo.Obrisi(id);

            if (!obrisana)
                return false;

            await _cuvanje.SacuvajPromene();

            return true;
        }

        public async Task<Zgrada?> VratiPoIdu(int id)
        {
            var vratiPodatke = await _repo.VratiPoIdu(id);
            if (vratiPodatke is null)
            {
                return null;
            }
            await _cuvanje.SacuvajPromene();
            return vratiPodatke;
        }

        public async Task<List<Zgrada>> Vratisve()
        {
            var vratiSvePodatke = await _repo.Vratisve(); 
            if(vratiSvePodatke is null)
            {
                return null;
            }
            await _cuvanje.SacuvajPromene();
            return vratiSvePodatke;
        }
    }
}
