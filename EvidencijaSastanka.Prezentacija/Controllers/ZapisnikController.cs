using EvidencijaSastanka.DTO.SastanakDTO;
using EvidencijaSastanka.DTO.ZapisnikDTO;
using EvidencijaSastanka.DTO.ZgradaDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace EvidencijaSastanka.Prezentacija.Controllers
{
    public class ZapisnikController : Controller
    {
        private readonly HttpClient _httpClient;

        public ZapisnikController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index(int? zgradaId)
        {
            await UcitajZgrade(zgradaId);

            var url = zgradaId.HasValue && zgradaId.Value > 0
                ? $"api/SastanakKontroler/VratiPoZgradi/{zgradaId.Value}"
                : "api/SastanakKontroler/Sve";

            var sastanci = await _httpClient.GetFromJsonAsync<List<PrikazSastankaDTO>>(url);

            return View(sastanci ?? new List<PrikazSastankaDTO>());
        }

        public async Task<IActionResult> Stampa(int id)
        {
            var zapisnik = await _httpClient.GetFromJsonAsync<ZapisnikDTO>(
                $"api/SastanakKontroler/Zapisnik/{id}"
            );

            if (zapisnik is null)
                return RedirectToAction(nameof(Index));

            return View(zapisnik);
        }

        private async Task UcitajZgrade(int? izabranaZgradaId = null)
        {
            var zgrade = await _httpClient.GetFromJsonAsync<List<PrikazZgradeDTO>>("api/ZgradaKontroler/Sve");

            ViewBag.Zgrade = new SelectList(
                zgrade ?? new List<PrikazZgradeDTO>(),
                "Id",
                "Naziv",
                izabranaZgradaId
            );
        }
    }
}