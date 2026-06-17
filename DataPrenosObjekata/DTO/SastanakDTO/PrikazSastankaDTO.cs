using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaSastanka.DTO.SastanakDTO
{
    public class PrikazSastankaDTO
    {
        public int Id { get; set; }
         
        public DateTime DatumSastanka { get; set; }

        public string Tema { get; set; } = string.Empty;

        public int BrojPrisutnih { get; set; }

        public decimal ProcenatPrisutnih { get; set; }

        public string StatusSastanka { get; set; } = string.Empty;

        public string? Zakljucak { get; set; }

        public int ZgradaId { get; set; }

        public string NazivZgrade { get; set; } = string.Empty;
    }
}
