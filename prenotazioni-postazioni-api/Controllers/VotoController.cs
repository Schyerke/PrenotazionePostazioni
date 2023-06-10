using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/voti")]
    public class VotoController : ControllerBase
    {
        private VotoService _votoService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(VotoController));

        public VotoController(VotoService votoService)
        {
            this._votoService = votoService;
        }
        /// <summary>
        /// Serve per ottenere l'elenco di votazioni effettuate da un utente verso gli altri
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [HttpGet]
        [Route("getVotiFromUtente")]
        public IActionResult GetVotiFromUtente(int idUtente)
        {
            try
            {
                _logger.Info("Id utente: " + idUtente);
                _logger.Info("Trovando tutti i voti effettuati di un utente...");
                List<Voto> voti = _votoService.GetVotiFromUtente(idUtente);
                _logger.Info("Voti dell'utente trovati con successo!");
                return Ok(voti);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Serve per ottenere l'elenco di tutte le votazioni che un utente ha ricevuto
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>
        /// Restituisce una lista di voti in caso di ricerca con esito positivo
        /// </returns>
        [Route("getVotiToUtente")]
        [HttpGet]
        public IActionResult GetVotiToUtente(int idUtente)
        {
            try
            {
                _logger.Info("Id utente: " + idUtente);
                _logger.Info("Trovando tutti i voti che sono stati effettuati su un utente...");
                List<Voto> voti = _votoService.GetVotiToUtente(idUtente);
                _logger.Info("Voti trovati con successo!");
                return Ok(voti);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una lista di utenti votati da un altro utente
        /// </summary>
        /// <param name="votoDto"></param>
        /// <returns></returns>
        [Route("makeVotoToUtente")]
        [HttpPost]
        public IActionResult MakeVotoToUtente([FromBody] VotoDto votoDto)
        {
            try
            {
                _logger.Info("Voto utente: " + votoDto.Utente.Nome);
                _logger.Info("Voto utente votato: " + votoDto.UtenteVotato.Nome);
                _logger.Info("Effettuando un voto su un utente...");
                _votoService.MakeVotoToUtente(votoDto);
                _logger.Info("Voto effettuato con successo!");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Info("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("deleteVoto")]
        public IActionResult DeleteVoto(int idUtente, int idUtenteVotato)
        {
            try
            {
                _logger.Info("Id utente :" + idUtente);
                _logger.Info("Id utente votato: " + idUtenteVotato);
                _logger.Info($"Eliminazione voto di {idUtente} verso {idUtenteVotato}");
                _votoService.DeleteVoto(idUtente, idUtenteVotato);
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Info("Voto not found: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}

