using EvidencijaSastanka.DTO.SastanakDTO;
using EvidencijaSastanka.DTO.StanarDTO;
using EvidencijaSastanka.DTO.ZgradaDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace EvidencijaSastanka.Prezentacija.Controllers
{
    public class SastanakController : Controller
    {
        private readonly HttpClient _httpClient;

        public SastanakController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/SastanakKontroler/Sve");

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                return Content($"API greška: {(int)response.StatusCode} - {response.ReasonPhrase}\n\n{greska}");
            }

            var sastanci = await response.Content.ReadFromJsonAsync<List<PrikazSastankaDTO>>();

            return View(sastanci ?? new List<PrikazSastankaDTO>());
        }

        public async Task<IActionResult> Dodaj(int? zgradaId)
        {
            await UcitajZgrade(zgradaId);

            if (zgradaId.HasValue)
                await UcitajStanarePoZgradi(zgradaId.Value);
            else
                ViewBag.Stanari = new List<PrikazStanaraDTO>();

            return View(new SastanakDodavanjeDTO
            {
                DatumSastanka = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    0
                ),
                ZgradaId = zgradaId ?? 0
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj(SastanakDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await UcitajZgrade(dto.ZgradaId);
                await UcitajStanarePoZgradi(dto.ZgradaId);
                return View(dto);
            }

            var response = await _httpClient.PostAsJsonAsync("api/SastanakKontroler/Dodaj", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var greska = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", greska);

            await UcitajZgrade(dto.ZgradaId);
            await UcitajStanarePoZgradi(dto.ZgradaId);

            return View(dto);
        }

        public async Task<IActionResult> Izmeni(int id)
        {
            var response = await _httpClient.GetAsync($"api/SastanakKontroler/VratiPoIdu/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var sastanak = await response.Content.ReadFromJsonAsync<PrikazSastankaDTO>();

            if (sastanak is null)
                return RedirectToAction(nameof(Index));

            await UcitajZgrade(sastanak.ZgradaId);

            return View(sastanak);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Izmeni(int id, PrikazSastankaDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await UcitajZgrade(dto.ZgradaId);
                return View(dto);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/SastanakKontroler/Izmeni/{id}", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var greska = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", greska);

            await UcitajZgrade(dto.ZgradaId);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Obrisi(int id)
        {
            await _httpClient.DeleteAsync($"api/SastanakKontroler/Obrisi/{id}");
            return RedirectToAction(nameof(Index));
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