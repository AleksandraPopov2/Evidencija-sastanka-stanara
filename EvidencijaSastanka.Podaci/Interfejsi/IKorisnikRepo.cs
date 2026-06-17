using EvidencijaSastanka.Modeli.Modeli;

namespace EvidencijaSastanka.Podaci.Interfejsi
{
    public interface IKorisnikRepo
    {
        Task<Korisnik?> Login(string korisnickoIme, string lozinka);
    }
}