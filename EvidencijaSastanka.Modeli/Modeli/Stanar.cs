using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Modeli.Modeli
{
    public class Stanar : Osoba
    {
        public int Id { get; set; }

        public int BrojStana { get; set; }

        public string? Email { get; set; }

        public int ZgradaId { get; set; }

        public Zgrada Zgrada { get; set; } = null!;
    }
}
