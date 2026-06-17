using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EvidencijaSastanka.DTO.ZapisnikDTO
{
    public class StanarNaZapisnikuDTO
    {
        public int RedniBroj { get; set; }
        public string ImePrezime { get; set; } = string.Empty;
        public int BrojStana { get; set; }  
        public bool Prisutan { get; set; }
    }
}
