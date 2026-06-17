using EvidencijaSastanka.DTO.SastanakDTO;
using EvidencijaSastanka.Modeli.Modeli;
using EvidencijaSastanka.Podaci.Interfejsi;
using EvidencijaSastanka.Servisi.Interfejsi;
using EvidencijaSastanka.Servisi.JedinicaZaRad;
using EvidencijaSastanka.DTO.ZapisnikDTO;
using EvidencijaSastanka.PoslovnaLogika;

namespace EvidencijaSastanka.Servisi.Servisi
{
    public class SastanakServis : ISastanakServis
    {
        private readonly ISastanakRepo _sastanakRepo;
        private readonly IPrisustvoNaSastankuRepo _prisustvoRepo;
        private readonly IRadSaCuvanjem _radSaCuvanjem;
        private readonly IStanarRepo _stanarRepo;
        private readonly PraviloValidnostiSastanka _praviloValidnostiSastanka;

        public SastanakServis(
      ISastanakRepo sastanakRepo,
      IPrisustvoNaSastankuRepo prisustvoRepo,
      IStanarRepo stanarRepo,
      IRadSaCuvanjem radSaCuvanjem)
        {
            _sastanakRepo = sastanakRepo;
            _prisustvoRepo = prisustvoRepo;
            _stanarRepo = stanarRepo;
            _radSaCuvanjem = radSaCuvanjem;
            _praviloValidnostiSastanka = new PraviloValidnostiSastanka();
        }

        public async Task<List<PrikazSastankaDTO>> Vratisve()
        {
            var sastanci = await _sastanakRepo.Vratisve();

            return sastanci.Select(MapirajUPrikazDTO).ToList();
        }

        public async Task<PrikazSastankaDTO?> VratiPoIdu(int id)
        {
            var sastanak = await _sastanakRepo.VratiPoIdu(id);

            if (sastanak is null)
                return null;

            return MapirajUPrikazDTO(sastanak);
        }

        public async Task<PrikazSastankaDTO> Dodaj(SastanakDodavanjeDTO dto)
        {
            var ukupanBrojStanara = await _stanarRepo.PrebrojPoZgradi(dto.ZgradaId);
            var brojPrisutnih = dto.PrisutniStanariIds.Count;

            var validan = _praviloValidnostiSastanka.DaLiJeSastanakValidan(
                brojPrisutnih,
                ukupanBrojStanara
            );

            if (!validan)
            {
                throw new InvalidOperationException(
                    "Sastanak nije moguće sačuvati jer je prisutno manje od 50% ukupnog broja stanara u zgradi."
                );
            }

            var procenat = _praviloValidnostiSastanka.IzracunajProcenat(
                brojPrisutnih,
                ukupanBrojStanara
            );

            var sastanak = new Sastanak
            {
                DatumSastanka = dto.DatumSastanka,
                Tema = dto.Tema,
                Zakljucak = dto.Zakljucak,
                ZgradaId = dto.ZgradaId,
                BrojPrisutnih = brojPrisutnih,
                ProcenatPrisutnih = procenat,
                StatusSastanka = "Validan"
            };

            await _sastanakRepo.Dodaj(sastanak);
            await _radSaCuvanjem.SacuvajPromene();

            foreach (var stanarId in dto.PrisutniStanariIds)
            {
                var prisustvo = new PrisustvoNaSastanku
                {
                    SastanakId = sastanak.Id,
                    StanarId = stanarId,
                    Prisutan = true
                };

                await _prisustvoRepo.Dodaj(prisustvo);
            }

            await _radSaCuvanjem.SacuvajPromene();

            var sacuvanSastanak = await _sastanakRepo.VratiPoIdu(sastanak.Id);

            return MapirajUPrikazDTO(sacuvanSastanak!);
        }

        public async Task<PrikazSastankaDTO?> Izmeni(int id, PrikazSastankaDTO dto)
        {
            var postojeciSastanak = await _sastanakRepo.VratiPoIdu(id);

            if (postojeciSastanak is null)
                return null;

            postojeciSastanak.DatumSastanka = dto.DatumSastanka;
            postojeciSastanak.Tema = dto.Tema;
            postojeciSastanak.Zakljucak = dto.Zakljucak;
            postojeciSastanak.ZgradaId = dto.ZgradaId;

            var sastanak = await _sastanakRepo.Izmeni(id, postojeciSastanak);

            if (sastanak is null)
                return null;

            await _radSaCuvanjem.SacuvajPromene();

            var sacuvanSastanak = await _sastanakRepo.VratiPoIdu(id);

            return MapirajUPrikazDTO(sacuvanSastanak!);
        }

        public async Task<bool> Obrisi(int id)
        {
            var obrisan = await _sastanakRepo.Obrisi(id);

            if (!obrisan)
                return false;

            await _radSaCuvanjem.SacuvajPromene();

            return true;
        }

        public async Task<List<PrikazSastankaDTO>> VratiPoZgradi(int zgradaId)
        {
            var sastanci = await _sastanakRepo.VratiPoZgradi(zgradaId);

            return sastanci.Select(MapirajUPrikazDTO).ToList();
        }

        private static PrikazSastankaDTO MapirajUPrikazDTO(Sastanak sastanak)
        {
            return new PrikazSastankaDTO
            {
                Id = sastanak.Id,
                DatumSastanka = sastanak.DatumSastanka,
                Tema = sastanak.Tema,
                BrojPrisutnih = sastanak.BrojPrisutnih,
                ProcenatPrisutnih = sastanak.ProcenatPrisutnih,
                StatusSastanka = sastanak.StatusSastanka,
                Zakljucak = sastanak.Zakljucak,
                ZgradaId = sastanak.ZgradaId,
                NazivZgrade = sastanak.Zgrada != null ? sastanak.Zgrada.Naziv : string.Empty
            };
        }
        public async Task<ZapisnikDTO?> VratiZapisnik(int sastanakId)
        {
            var sastanak = await _sastanakRepo.VratiZaZapisnik(sastanakId);

            if (sastanak is null)
                return null;

            var stanari = sastanak.Prisustva
                .Where(p => p.Prisutan)
                .Select((p, index) => new StanarNaZapisnikuDTO
                {
                    RedniBroj = index + 1,
                    ImePrezime = $"{p.Stanar.Ime} {p.Stanar.Prezime}",
                    BrojStana = p.Stanar.BrojStana,
                    Prisutan = p.Prisutan
                })
                .ToList();

            return new ZapisnikDTO
            {
                IdSastanka = sastanak.Id,
                DatumSastanka = sastanak.DatumSastanka,
                NazivZgrade = sastanak.Zgrada != null ? sastanak.Zgrada.Naziv : string.Empty,
                Tema = sastanak.Tema,
                StatusSastanka = sastanak.StatusSastanka,
                Zakljucak = sastanak.Zakljucak,
                Stanari = stanari
            };
        }
    }
}