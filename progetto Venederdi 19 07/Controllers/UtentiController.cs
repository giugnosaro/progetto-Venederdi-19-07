using Microsoft.AspNetCore.Mvc;
using progetto_Venederdi_19_07.ViewModels;
using progetto_Venederdi_19_07.Services;

namespace progetto_Venederdi_19_07.Controllers
{
    public class UtentiController : Controller
    {
        public readonly UtentiService _utentiService;

        public UtentiController(UtentiService utentiService)
        {
            _utentiService = utentiService;
        }

        public IActionResult Index()
        {

            List<UtentiViewModel> utenti = _utentiService.NuovoUtente();

            return View(utenti);
        }

        public IActionResult NuovoUtente()
        {   
            ViewBag.Utenti = _utentiService.NuovoUtente();

            return View();
        }

        [HttpPost]

        public IActionResult NuovoUtente(UtentiViewModel utente)
        {
            _utentiService.NuovoUtente(utente);

            return RedirectToAction("Index");
        }

    }
}
