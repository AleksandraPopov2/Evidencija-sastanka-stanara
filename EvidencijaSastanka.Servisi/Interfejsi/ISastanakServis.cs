using EvidencijaSastanka.DTO.SastanakDTO;
using EvidencijaSastanka.DTO.ZapisnikDTO;

namespace EvidencijaSastanka.Servisi.Interfejsi
{
    public interface ISastanakServis
    {
        Task<List<PrikazSastankaDTO>> Vratisve();
        Task<PrikazSastankaDTO?> VratiPoIdu(int id);
        Task<PrikazSastankaDTO> Dodaj(SastanakDodavanjeDTO dto);
        Task<PrikazSastankaDTO?> Izmeni(int id, PrikazSastankaDTO dto);
        Task<bool> Obrisi(int id);
        Task<List<PrikazSastankaDTO>> VratiPoZgradi(int zgradaId);
        Task<ZapisnikDTO?> VratiZapisnik(int sastanakId);
    }
}