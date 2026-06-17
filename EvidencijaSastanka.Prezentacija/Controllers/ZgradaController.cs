using EvidencijaSastanka.DTO.ZgradaDTO;
using EvidencijaSastanka.Podaci.Repozitorijumi;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace EvidencijaSastanka.Prezentacija.Controllers
{
    public class ZgradaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ZgradaController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/ZgradaKontroler/Sve");

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                return Content($"API greška: {(int)response.StatusCode} - {response.ReasonPhrase}\n\n{greska}");
            }

            var zgrade = await response.Content.ReadFromJsonAsync<List<PrikazZgradeDTO>>();

            return View(zgrade ?? new List<PrikazZgradeDTO>());
        }

        public async Task<IActionResult> DBUtilsPregled()
        {
            var repo = new ZgradaRepoDBUtils(_configuration);
            var zgrade = await repo.VratiSveZgrade();

            return View(zgrade);
        }

        public IActionResult Dodaj()
        {
            return View(new ZgradaDodavanjeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj(ZgradaDodavanjeDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var response = await _httpClient.PostAsJsonAsync("api/ZgradaKontroler/Dodaj", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var greska = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Greška prilikom dodavanja zgrade: {greska}");

            return View(dto);
        }

        public async Task<IActionResult> Izmeni(int id)
        {
            var response = await _httpClient.GetAsync($"api/ZgradaKontroler/VratiPoIdu/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var zgrada = await response.Content.ReadFromJsonAsync<PrikazZgradeDTO>();

            if (zgrada is null)
                return RedirectToAction(nameof(Index));

            return View(zgrada);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Izmeni(int id, PrikazZgradeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID iz rute i ID iz forme se ne poklapaju.");

            if (!ModelState.IsValid)
                return View(dto);

            var response = await _httpClient.PutAsJsonAsync($"api/ZgradaKontroler/Izmeni/{id}", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var greska = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Greška prilikom izmene zgrade: {greska}");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Obrisi(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ZgradaKontroler/Obrisi/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var greska = await response.Content.ReadAsStringAsync();
                TempData["Greska"] = $"Greška prilikom brisanja zgrade: {greska}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}