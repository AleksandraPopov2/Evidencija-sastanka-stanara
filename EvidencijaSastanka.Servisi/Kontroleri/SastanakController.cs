using EvidencijaSastanka.DTO.SastanakDTO;
using EvidencijaSastanka.Servisi.Interfejsi;
using EvidencijaSastanka.Servisi.Servisi;
using Microsoft.AspNetCore.Mvc;

namespace EvidencijaSastanka.Servisi.Kontroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class SastanakKontroler : ControllerBase
    {
        private readonly ISastanakServis _sastanakServis;

        public SastanakKontroler(ISastanakServis sastanakServis)
        {
            _sastanakServis = sastanakServis;
        }

        [HttpGet("Sve")]
        public async Task<IActionResult> VratiSve()
        {
            var sastanci = await _sastanakServis.Vratisve();
            return Ok(sastanci);
        }

        [HttpGet("VratiPoIdu/{id}")]
        public async Task<IActionResult> VratiPoIdu(int id)
        {
            var sastanak = await _sastanakServis.VratiPoIdu(id);

            if (sastanak is null)
                return NotFound("Sastanak nije pronađen.");

            return Ok(sastanak);
        }

        [HttpGet("VratiPoZgradi/{zgradaId}")]
        public async Task<IActionResult> VratiPoZgradi(int zgradaId)
        {
            var sastanci = await _sastanakServis.VratiPoZgradi(zgradaId);
            return Ok(sastanci);
        }

        
        [HttpPost("Dodaj")]
        public async Task<IActionResult> Dodaj([FromBody] SastanakDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var sastanak = await _sastanakServis.Dodaj(dto);
                return Ok(sastanak);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Izmeni/{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] PrikazSastankaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sastanak = await _sastanakServis.Izmeni(id, dto);

            if (sastanak is null)
                return NotFound("Sastanak nije pronađen.");

            return Ok(sastanak);
        }

        [HttpDelete("Obrisi/{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var obrisan = await _sastanakServis.Obrisi(id);

            if (!obrisan)
                return NotFound("Sastanak nije pronađen.");

            return Ok(obrisan);
        }

        [HttpGet("Zapisnik/{id}")]
        public async Task<IActionResult> VratiZapisnik(int id)
        {
            var zapisnik = await _sastanakServis.VratiZapisnik(id);

            if (zapisnik is null)
                return NotFound("Zapisnik nije pronađen.");

            return Ok(zapisnik);
        }
    }
}