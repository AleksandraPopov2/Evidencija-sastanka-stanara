using EvidencijaSastanka.Modeli.Modeli;

namespace EvidencijaSastanka.Podaci.Interfejsi
{
    public interface ISastanakRepo
    {
        Task<List<Sastanak>> Vratisve();

        Task<Sastanak?> VratiPoIdu(int id);

        Task<Sastanak> Dodaj(Sastanak sastanak);

        Task<Sastanak?> Izmeni(int id, Sastanak izmenjenSastanak);

        Task<bool> Obrisi(int id);

        Task<List<Sastanak>> VratiPoZgradi(int zgradaId);

        Task<Sastanak?> VratiZaZapisnik(int id);
    }
}