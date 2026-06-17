using EvidencijaSastanka.DTO.StanarDTO;
using EvidencijaSastanka.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;

namespace EvidencijaSastanka.Servisi.Kontroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class StanarKontroler : ControllerBase
    {
        private readonly IStanarServis _stanarServis;

        public StanarKontroler(IStanarServis stanarServis)
        {
            _stanarServis = stanarServis;
        }

        [HttpGet("Sve")]
        public async Task<IActionResult> VratiSve()
        {
            var stanari = await _stanarServis.Vratisve();
            return Ok(stanari);
        }

        [HttpGet("VratiPoIdu/{id}")]
        public async Task<IActionResult> VratiPoIdu(int id)
        {
            var stanar = await _stanarServis.VratiPoIdu(id);

            if (stanar is null)
                return NotFound("Stanar nije pronađen.");

            return Ok(stanar);
        }

        [HttpPost("Dodaj")]
        public async Task<IActionResult> Dodaj([FromBody] StanarDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stanar = await _stanarServis.Dodaj(dto);
            return Ok(stanar);
        }

        [HttpPut("Izmeni/{id}")]
        public async Task<IActionResult> Izmeni(int id, [FromBody] PrikazStanaraDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stanar = await _stanarServis.Izmeni(id, dto);

            if (stanar is null)
                return NotFound("Stanar nije pronađen.");

            return Ok(stanar);
        }

        [HttpDelete("Obrisi/{id}")]
        public async Task<IActionResult> Obrisi(int id)
        {
            var obrisan = await _stanarServis.Obrisi(id);

            if (!obrisan)
                return NotFound("Stanar nije pronađen.");

            return Ok(obrisan);
        }
        [HttpGet("PoZgradi/{zgradaId}")]
        public async Task<IActionResult> VratiPoZgradi(int zgradaId)
        {
            var stanari = await _stanarServis.VratiPoZgradi(zgradaId);
            return Ok(stanari);
        }
    }
}