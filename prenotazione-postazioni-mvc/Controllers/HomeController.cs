using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using prenotazione_postazioni_mvc.Models;

namespace prenotazione_postazioni_mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public static PrenotazioneViewModel ViewModel { get; set; }

    public IActionResult Index()
    {

        if (ViewModel == null)
            ViewModel = new PrenotazioneViewModel();

        return View(ViewModel);
    }

    [HttpPost]
    [ActionName("ReloadDay")]
    public IActionResult ReloadDay(int year, int month, int day)
    {

        ViewModel.Date = new DateTime(year, month, day);
        ViewModel.Start = new DateTime(year, month, day, ViewModel.Start.Hour, 0, 0);
        ViewModel.End = new DateTime(year, month, day, ViewModel.End.Hour, 0, 0);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ActionName("ReloadRoom")]
    public IActionResult ReloadRoom(string room)
    {
        ViewModel.Stanza = room;

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ActionName("ReloadStart")]
    public IActionResult ReloadStart(int hour)
    {

        ViewModel.Start = new DateTime(ViewModel.Date.Year, ViewModel.Date.Month, ViewModel.Date.Day, hour, 0, 0);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ActionName("ReloadFinish")]
    public IActionResult ReloadFinish(int hour)
    {

        ViewModel.End = new DateTime(ViewModel.Date.Year, ViewModel.Date.Month, ViewModel.Date.Day, hour, 0, 0);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ActionName("CollapseHour")]
    public void CollapseHour(int collapse)
    {
        ViewModel.CollapsedHour = collapse;
    }
}
