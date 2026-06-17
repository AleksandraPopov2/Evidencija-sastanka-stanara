using EvidencijaSastanka.DTO.ZgradaDTO;
using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Servisi.DTO.ZgradaDTO;

namespace EvidencijaSastanka.Servisi.Interfejsi
{
    public interface IZgradaServis
    {
        public Task<List<Zgrada>> Vratisve();
        public Task<Zgrada?> VratiPoIdu(int id);
        public Task<ZgradaDodavanjeDTO> Dodaj(ZgradaDodavanjeDTO zgrada);
        public Task<bool> Obrisi(int id);
        public Task<PrikazZgradeDTO?> Izmeni(int id, PrikazZgradeDTO dto);
    }
}
