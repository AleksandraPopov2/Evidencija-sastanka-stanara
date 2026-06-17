using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.DTO.StanarDTO
{
    public class StanarDodavanjeDTO
    {
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int BrojStana { get; set; } 
        public int ZgradaId { get; set; }
    }
}
