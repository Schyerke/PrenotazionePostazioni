using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers
{
    public class VotazioniController : Controller
    {

        public static VotazioniViewModel ViewModel { get; set; }

        public IActionResult Index()
        {
            if(ViewModel == null)
                ViewModel = new VotazioniViewModel();
            return View(ViewModel);
        }

        [HttpPost]
        [ActionName("VoteUser")]
        public IActionResult VoteUser(int voto, int i)
        {
            try
            {
                ViewModel.Votazioni[i] = voto;
            } catch(Exception)
            {
                ///controllo se sono presenti elementi prima della posizione in cui si vuole inserire il voto
                if (i != ViewModel.Votazioni.Count)
                {
                    ///il numero di cicli rappresenta il numero di posizioni da riempire (con un voto nullo)
                    int cicli = i - ViewModel.Votazioni.Count;
                    for (int j = 0; j < cicli; j++)
                    {
                        ViewModel.Votazioni.Add(0);
                    }
                }
                ViewModel.Votazioni.Add(voto);
            }

            return RedirectToAction("Index");
        }
    }
}
