
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{

    [ApiController]
    [Route("/api/ruoli")]
    public class RuoloController : ControllerBase
    {
        private RuoloService _ruoloService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(RuoloController));

        public RuoloController(RuoloService ruoloService)
        {
            _ruoloService = ruoloService;
        }




        /// <summary>
        /// Restituisce il ruolo di un utente mediante l'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente per trovare il ruolo associato ad esso</param>
        /// <returns>L'utente trovato con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getRuoloUtente")]
        public IActionResult GetRuoloUtenteById(int idRuolo)
        {
            try
            {
                _logger.Info("Id ruolo: " + idRuolo);
                _logger.Info("Prelevando un ruolo mediante Ruolo Id...");
                Ruolo ruolo = _ruoloService.GetRuoloById(idRuolo);
                _logger.Info("Ruolo prelevato con successo!");
                return Ok(ruolo);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve a ottenere il Ruolo di un utente tramite l'id dell'utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Il ruolo e stato 200 in caso di ricerca effettuata con successo, 404 altrimenti</returns>
        [HttpGet]
        [Route("getRuoloByIdUtente")]
        public IActionResult GetRuoloUtenteByIdUtente(int idUtente)
        {
            try
            {
                _logger.Info("Id utente: " + idUtente);
                _logger.Info("Trovando un ruolo mediante l'id dell'utente...");
                Ruolo ruolo = _ruoloService.GetRuoloByIdUtente(idUtente);
                _logger.Info("Ruolo dell'id utente: " + idUtente + " trovato con successo!");
                return Ok(ruolo);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Ruolo non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna il ruolo di un utente dall'admin
        /// </summary>
        /// <param name="utenteAdminDto">L'utente che gli verra aggiornato il ruolo</param>
        /// <returns>Ok, Not Authorized altrimenti</returns>

        [HttpPost]
        [Route("updateRuoloUtenteByUtenteId")]
        public IActionResult UpdateRuoloUtenteByAdminUtenteId(int idUtente,int idAdmin)
        {
            try
            {
                _logger.Info("Id Utente: " + idUtente);
                _logger.Info("Id admin: " + idAdmin);
                _logger.Info("Aggiornando il ruolo di un utente...");
                bool ok = _ruoloService.UpdateRuoloUtenteByAdminUtenteId(idUtente, idAdmin);
                _logger.Info("Controllando se l'autorizzazione e' valida...");
                if (ok)
                {
                    _logger.Info("Autorizzazione riconosciuta!");
                    return Ok();
                }
                else
                {
                    _logger.Error("Autorizzazione NON riconosciuta");
                    return Forbid("Non autorizzato");
                }
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}