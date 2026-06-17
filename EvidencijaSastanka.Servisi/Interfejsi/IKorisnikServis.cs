using EvidencijaSastanka.DTO.KorisnikDTO;

namespace EvidencijaSastanka.Servisi.Interfejsi
{
    public interface IKorisnikServis
    {
        Task<PrikazKorisnikaDTO?> Login(PrijavaDTO dto);
    }
}