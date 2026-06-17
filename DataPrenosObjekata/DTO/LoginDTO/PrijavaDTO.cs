using System.ComponentModel.DataAnnotations;

namespace EvidencijaSastanka.DTO.KorisnikDTO
{
    public class PrijavaDTO 
    {
        [Required]
        public string KorisnickoIme { get; set; } = string.Empty;

        [Required]
        public string Lozinka { get; set; } = string.Empty;
    }
}