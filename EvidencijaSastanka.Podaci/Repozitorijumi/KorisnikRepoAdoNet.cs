using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class KorisnikRepoAdoNet : IKorisnikRepo
    {
        private readonly string _konekcija;

        public KorisnikRepoAdoNet(IConfiguration configuration)
        {
            _konekcija = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Korisnik?> Login(string korisnickoIme, string lozinka)
        {
            using var connection = new SqlConnection(_konekcija);
            using var command = new SqlCommand("LoginKorisnika", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@KorisnickoIme", korisnickoIme);
            command.Parameters.AddWithValue("@Lozinka", lozinka);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new Korisnik
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                KorisnickoIme = reader.GetString(reader.GetOrdinal("KorisnickoIme")),
                Ime = reader.GetString(reader.GetOrdinal("Ime")),
                Prezime = reader.GetString(reader.GetOrdinal("Prezime"))
            };
        }
    }
}