using EvidencijaSastanka.DTO.StanarDTO;
using EvidencijaSastanka.DTO.ZgradaDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace EvidencijaSastanka.Prezentacija.Controllers
{
    public class StanarController : Controller
    {
        private readonly HttpClient _httpClient;

        public StanarController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/StanarKontroler/Sve");

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                return Content($"API greška: {(int)response.StatusCode} - {response.ReasonPhrase}\n\n{greska}");
            }

            var stanari = await response.Content.ReadFromJsonAsync<List<PrikazStanaraDTO>>();

            return View(stanari);
        }

        public async Task<IActionResult> Dodaj()
        {
            await UcitajZgrade();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj(StanarDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await UcitajZgrade();
                return View(dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/StanarKontroler/Dodaj", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Greška prilikom dodavanja stanara.");
            await UcitajZgrade();
            return View(dto);
        }

        public async Task<IActionResult> Izmeni(int id)
        {
            var stanar = await _httpClient.GetFromJsonAsync<PrikazStanaraDTO>(
                $"api/StanarKontroler/VratiPoIdu/{id}"
            );

            if (stanar == null)
                return NotFound();

            await UcitajZgrade();
            return View(stanar);
        }

        [HttpPost]
        public async Task<IActionResult> Izmeni(int id, PrikazStanaraDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await UcitajZgrade();
                return View(dto);
            }

            var response = await _httpClient.PutAsJsonAsync(
                $"api/StanarKontroler/Izmeni/{id}",
                dto
            );

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Greška prilikom izmene stanara.");
            await UcitajZgrade();
            return View(dto);
        }

        public async Task<IActionResult> Obrisi(int id)
        {
            await _httpClient.DeleteAsync($"api/StanarKontroler/Obrisi/{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task UcitajZgrade()
        {
            var zgrade = await _httpClient.GetFromJsonAsync<List<PrikazZgradeDTO>>(
                "api/ZgradaKontroler/Sve"
            );

            ViewBag.Zgrade = new SelectList(zgrade, "Id", "Naziv");
        }
        private async Task UcitajStanarePoZgradi(int zgradaId)
        {
            var response = await _httpClient.GetAsync($"api/StanarKontroler/PoZgradi/{zgradaId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Stanari = new List<PrikazStanaraDTO>();
                return;
            }

            var stanari = await response.Content.ReadFromJsonAsync<List<PrikazStanaraDTO>>();
            ViewBag.Stanari = stanari ?? new List<PrikazStanaraDTO>();
        }
    }
}