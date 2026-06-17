using EvidencijaSastanka.Modeli.Modeli;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EvidencijaSastanka.Podaci.Repozitorijumi
{
    public class ZgradaRepoDBUtils : EvidencijaSastanka.Podaci.DBUtils.DBUtils
    {
        public ZgradaRepoDBUtils(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<List<Zgrada>> VratiSveZgrade()
        {
            string upit = "SELECT Id, Naziv, Adresa FROM Zgrada";

            DataTable tabela = await IzvrsiUpit(upit);

            List<Zgrada> rezultat = new();

            foreach (DataRow red in tabela.Rows)
            {
                rezultat.Add(new Zgrada
                {
                    Id = Convert.ToInt32(red["Id"]),
                    Naziv = red["Naziv"].ToString() ?? "",
                    Adresa = red["Adresa"].ToString() ?? ""
                });
            }

            return rezultat;
        }
    }
}