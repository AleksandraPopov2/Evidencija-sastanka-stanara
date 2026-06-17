using EvidencijaSastanka.DTO.StanarDTO;

namespace EvidencijaSastanka.Servisi.Interfejsi
{
    public interface IStanarServis
    {
        Task<List<PrikazStanaraDTO>> Vratisve();

        Task<PrikazStanaraDTO?> VratiPoIdu(int id);

        Task<PrikazStanaraDTO> Dodaj(StanarDodavanjeDTO dto);

        Task<PrikazStanaraDTO?> Izmeni(int id, PrikazStanaraDTO dto);

        Task<bool> Obrisi(int id);
        Task<List<PrikazStanaraDTO>> VratiPoZgradi(int zgradaId);
    }
}