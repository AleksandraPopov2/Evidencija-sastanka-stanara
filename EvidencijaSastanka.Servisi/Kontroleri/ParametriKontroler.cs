using EvidencijaSastanka.PoslovnaLogika;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EvidencijaSastanka.Servisi.Kontroleri
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametriKontroler : ControllerBase
    {
        [HttpGet("MinimalniProcenatPrisutnih")]
        public IActionResult VratiMinimalniProcenatPrisutnih()
        {
            var putanja = Path.Combine(
                AppContext.BaseDirectory,
                "pravila-sistema.json"
            );

            if (!System.IO.File.Exists(putanja))
                return Ok(50);

            var json = System.IO.File.ReadAllText(putanja);

            var parametri =
                JsonSerializer.Deserialize<ParametriSastanka>(json);

            return Ok(parametri?.MinimalniProcenatPrisutnih ?? 50);
        }
    }
}