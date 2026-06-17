using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EvidencijaSastanka.Podaci.DBUtils
{
    public abstract class DBUtils
    {
        protected readonly string _konekcija;

        protected DBUtils(IConfiguration configuration)
        {
            _konekcija = configuration.GetConnectionString("DefaultConnection")!;
        }

        protected async Task<DataTable> IzvrsiUpit(string upit)
        {
            using var konekcija = new SqlConnection(_konekcija);
            using var komanda = new SqlCommand(upit, konekcija);

            await konekcija.OpenAsync();

            using var reader = await komanda.ExecuteReaderAsync();

            var tabela = new DataTable();
            tabela.Load(reader);

            return tabela;
        }
    }
}