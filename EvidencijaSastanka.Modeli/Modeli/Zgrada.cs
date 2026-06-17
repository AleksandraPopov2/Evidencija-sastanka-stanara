using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.Modeli.Modeli
{
    public class Zgrada
    {
        public int Id { get; set; }

        public string Naziv { get; set; } = string.Empty;

        public string Adresa { get; set; } = string.Empty;

        public List<Sastanak> Sastanci { get; set; } = new();
        public List<Stanar> Stanari { get; set; } = new();
    }
}
