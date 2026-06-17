using EvidencijaSastanka.Modeli.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Podaci.Interfejsi
{
    public interface IStanarRepo
    {
        public Task<List<Stanar>> Vratisve();
        public Task<Stanar?> VratiPoIdu(int id);
        public Task<Stanar> Dodaj(Stanar stanar);
        public Task<bool> Obrisi(int id);
        public Task<Stanar?> Izmeni(int id, Stanar izmenjenStanar);
        Task<List<Stanar>> VratiPoZgradi(int zgradaId);
        Task<int> PrebrojPoZgradi(int zgradaId);
    }
}
