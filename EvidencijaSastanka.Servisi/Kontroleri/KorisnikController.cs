using EvidencijaSastanka.DTO.KorisnikDTO;
using EvidencijaSastanka.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;

namespace EvidencijaSastanka.Servisi.Kontroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikKontroler : ControllerBase
    {
        private readonly IKorisnikServis _korisnikServis;

        public KorisnikKontroler(IKorisnikServis korisnikServis)
        {
            _korisnikServis = korisnikServis;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] PrijavaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var korisnik = await _korisnikServis.Login(dto);

            if (korisnik is null)
                return Unauthorized("Neispravno korisničko ime ili lozinka.");

            return Ok(korisnik);
        }
    }
}