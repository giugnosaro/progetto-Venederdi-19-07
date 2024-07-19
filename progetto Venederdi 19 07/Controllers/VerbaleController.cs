using Microsoft.AspNetCore.Mvc;
using progetto_Venederdi_19_07.Services;
using progetto_Venederdi_19_07.ViewModels;
using System.Collections.Generic;

namespace progetto_Venederdi_19_07.Controllers
{
    public class VerbaleController : Controller
    {
        public readonly UtentiService _utentiService;

        public readonly VerbaleService _verbaleService;

        public VerbaleController(VerbaleService verbaleService , UtentiService utentiService)
        {
            _utentiService = utentiService;
            _verbaleService = verbaleService;
        }

        public IActionResult Index()
        {

            List<VerbaleViewModel> verbali = _verbaleService.GetVerbali();
            
            return View(verbali);


            
        }

        public IActionResult NuovoVerbale()
        {

            ViewBag.Utenti = _utentiService.NuovoUtente();
            ViewBag.Violazioni = _verbaleService.TutteLeViolazioni();
            


            return View();
        }

        [HttpPost]
        public IActionResult NuovoVerbale(VerbaleViewModel verb)
        {  
            _verbaleService.NuovoVerbale(verb);

            

            return RedirectToAction("Index");
        }
        public IActionResult TotVerbali ()
    { 
        List<VerbaleViewModel> verbali = _verbaleService.TotVerbTrasgressore();
        return View(verbali);
        
    }

        public IActionResult TotPunti ()
        {
            List<VerbaleViewModel> verbali = _verbaleService.TotPuntiDecurtati();
            return View(verbali);
        }

        public IActionResult TotImporti ()
        {
            List<VerbaleViewModel> verbali = _verbaleService.VisualizzazionePiuDiDieciPunti();
            return View(verbali);
        }

        public IActionResult QuattroCentoEuro ()
        {
            List<VerbaleViewModel> verbali = _verbaleService.VisualizzazionePiuDiQuattroCentoEuro();
            return View(verbali);
        }

        public IActionResult Menu ()
        {
            
            return View();
        }
    }
}
