using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EvidencijaSastanka.Modeli.Modeli;

namespace EvidencijaSastanka.Podaci.Interfejsi
{
    public interface IPrisustvoNaSastankuRepo
    {
        Task Dodaj(PrisustvoNaSastanku prisustvo);
        Task<List<PrisustvoNaSastanku>> VratiPoSastanku(int sastanakId);
    }
}