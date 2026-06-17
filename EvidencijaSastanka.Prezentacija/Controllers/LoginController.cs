using EvidencijaSastanka.DTO.KorisnikDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace EvidencijaSastanka.Prezentacija.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public IActionResult Index()
        {
            return View(new PrijavaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PrijavaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var response = await _httpClient.PostAsJsonAsync("api/KorisnikKontroler/Login", dto);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ModelState.AddModelError("", "Neispravno korisničko ime ili lozinka.");
                return View(dto);
            }

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Greška prilikom prijave.");
                return View(dto);
            }

            var korisnik = await response.Content.ReadFromJsonAsync<PrikazKorisnikaDTO>();

            if (korisnik is null)
            {
                ModelState.AddModelError("", "Greška prilikom učitavanja korisnika.");
                return View(dto);
            }

            HttpContext.Session.SetInt32("KorisnikId", korisnik.Id);
            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            HttpContext.Session.SetString("ImePrezime", $"{korisnik.Ime} {korisnik.Prezime}");

            return RedirectToAction("Index", "Zgrada");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}