using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Modeli.Modeli
{
    public class PrisustvoNaSastanku : Osoba
    {
        public int Id { get; set; }

        public int SastanakId { get; set; }
        public Sastanak Sastanak { get; set; } = null!;

        public int StanarId { get; set; }
        public Stanar Stanar { get; set; } = null!;

        public bool Prisutan { get; set; }

    }
}
