using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EvidencijaSastanka.DTO.ZapisnikDTO
{
    public class ZapisnikDTO
    {
        public int IdSastanka { get; set; }
        public DateTime DatumSastanka { get; set; }
        public string NazivZgrade { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public string StatusSastanka { get; set; } = string.Empty;
        public string? Zakljucak { get; set; }

        public List<StanarNaZapisnikuDTO> Stanari { get; set; } = new();
    }
}
