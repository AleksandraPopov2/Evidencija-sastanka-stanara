using System.ComponentModel.DataAnnotations;

namespace EvidencijaSastanka.DTO.ZgradaDTO
{
    public class ZgradaDodavanjeDTO
    {
        
        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Adresa { get; set; } = string.Empty;
    }
}
