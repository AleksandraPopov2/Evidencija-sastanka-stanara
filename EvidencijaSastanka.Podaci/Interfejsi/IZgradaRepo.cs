using EvidencijaSastanka.Modeli.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Podaci.Interfejsi
{
    public  interface IZgradaRepo
    {
        public Task<List<Zgrada>> Vratisve();
        public Task<Zgrada?> VratiPoIdu(int id);
        public Task<Zgrada> Dodaj(Zgrada zgrada);
        public Task<bool> Obrisi(int id);
        public Task<Zgrada?> Izmeni(int id,Zgrada izmenjenaZgrada);
    }
}
