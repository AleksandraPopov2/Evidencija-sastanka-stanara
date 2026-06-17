using EvidencijaSastanka.DTO.ZgradaDTO;
using EvidencijaSastanka.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;

namespace EvidencijaSastanka.Servisi.Kontroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgradaKontroler : ControllerBase
    {
        private readonly IZgradaServis _zgradaServis;

        public ZgradaKontroler(IZgradaServis zgradaServis)
        {
            _zgradaServis = zgradaServis;
        }

        [HttpGet("Sve")]
        public async Task<IActionResult> VratiSve()
        {
            var zgrade = await _zgradaServis.Vratisve();
            return Ok(zgrade);
        }

        [HttpGet("VratiPoIdu/{id}")]
        public async Task<IActionResult> VratiPoIdu(int id)
        {
            var zgrada = await _zgradaServis.VratiPoIdu(id);

            if (zgrada is null)
                return NotFound("Zgrada nije pronađena.");

            return Ok(zgrada);
        }

        [HttpPost("Dodaj")]
        public async Task<IActionResult> Dodaj([FromBody] ZgradaDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var zgrada = await _zgradaServis.Dodaj(dto);
            return Ok(zgrada);
        }

        [HttpPut("Izmeni/{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] PrikazZgradeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var zgrada = await _zgradaServis.Izmeni(id, dto);

            if (zgrada is null)
                return NotFound("Zgrada nije pronađena.");

            return Ok(zgrada);
        }

        [HttpDelete("Obrisi/{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var obrisanaZgrada = await _zgradaServis.Obrisi(id);

            if (!obrisanaZgrada)
                return NotFound("Zgrada nije pronađena.");

            return Ok(obrisanaZgrada);
        }
    }
}