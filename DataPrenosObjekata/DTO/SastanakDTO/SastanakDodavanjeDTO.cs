using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.DTO.SastanakDTO
{
    public class SastanakDodavanjeDTO
    {
        public DateTime DatumSastanka { get; set; }

        public string Tema { get; set; } = string.Empty; 

        public string? Zakljucak { get; set; }

        public int ZgradaId { get; set; }

        public List<int> PrisutniStanariIds { get; set; } = new();
    }
}
