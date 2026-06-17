using System.Text.Json;

namespace EvidencijaSastanka.PoslovnaLogika
{
    public class PraviloValidnostiSastanka
    {
        public bool DaLiJeSastanakValidan(int brojPrisutnih, int ukupanBrojStanara)
        {
            if (ukupanBrojStanara <= 0)
                return false;

            var parametri = UcitajParametre();

            decimal procenat = ((decimal)brojPrisutnih / ukupanBrojStanara) * 100;

            return procenat >= parametri.MinimalniProcenatPrisutnih;
        }

        public decimal IzracunajProcenat(int brojPrisutnih, int ukupanBrojStanara)
        {
            if (ukupanBrojStanara <= 0)
                return 0;

            return Math.Round(((decimal)brojPrisutnih / ukupanBrojStanara) * 100, 2);
        }

        private ParametriSastanka UcitajParametre()
        {
            var putanja = Path.Combine(AppContext.BaseDirectory, "pravila-sistema.json");

            if (!File.Exists(putanja))
            {
                return new ParametriSastanka
                {
                    MinimalniProcenatPrisutnih = 50
                };
            }

            var json = File.ReadAllText(putanja);

            var parametri = JsonSerializer.Deserialize<ParametriSastanka>(json);

            return parametri ?? new ParametriSastanka
            {
                MinimalniProcenatPrisutnih = 50
            };
        }
    }
}