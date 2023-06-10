
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("api/prenotazioni")]
    public class PrenotazioneController : ControllerBase
    {
        private PrenotazioneService _prenotazioneService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(PrenotazioneController));

        public PrenotazioneController(PrenotazioneService prenotazioneService)
        {
            _prenotazioneService = prenotazioneService;
        }
        /// <summary>
        /// Restituisce la Prenotazione trovata mediante il suo ID
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione e status 200, status 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioneById")]
        public IActionResult GetPrenotazioneById(int idPrenotazione)
        {
            try
            {
                _logger.Info("Id Prenotazione: " + idPrenotazione);
                _logger.Info("Trovando una prenotazione mediante l'id...");
                _logger.Info("Id: " + idPrenotazione);
                Prenotazione prenotazione = _prenotazioneService.GetPrenotazioneById(idPrenotazione);
                _logger.Info("Trovato una prenotazione con id: " + prenotazione.IdPrenotazioni + " con successo");
                return Ok(prenotazione);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Prenotazione non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le prenotazioni presenti nel Database
        /// </summary>
        /// <returns>Lista di Prenotazioni e status 200</returns>
        [HttpGet]
        [Route("getAllPrenotazioni")]
        public IActionResult GetAllPrenotazioni()
        {
            try
            {
                _logger.Info("Trovando tutte le prenotazioni...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetAllPrenotazioni();
                _logger.Info("Prenotazioni trovate con successo!");
                return Ok(prenotazioni);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce la Prenotazione associata alla sua stanza.
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioniByStanza")]
        public IActionResult GetPrenotazioniByStanza(int idStanza)
        {
            try
            {
                _logger.Info("Id stanza: " + idStanza);
                _logger.Info("Trovando tutte le prenotazioni di una stanza...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetPrenotazioniByStanza(idStanza);
                _logger.Info("Prenotazioni della stanza ID: " + idStanza + " trovate!");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
        
        /// <summary>
        /// Restituisce tutte le Prenotazioni dall'Id utente associato
        /// </summary>
        /// <param name="idUtente">L'id utente associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione e status 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getPrenotazioniByUtente")]
        public IActionResult GetPrenotazioneByUtente(int idUtente)
        {
            try
            {
                _logger.Info("Id utente: " + idUtente);
                _logger.Info("Trovando tutte le prenotazioni di un utente");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetPrenotazioniByUtente(idUtente);
                _logger.Info("Prenotazioni dell'id utente: " + idUtente + " trovate con successo!");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutte le Prenotazioni effettuate in una Stanza
        /// </summary>
        /// <param name="idStanza"></param>
        /// <param name="date"></param>
        /// <returns>Lista di Prenotazioni e status 200, altrimenti 404</returns>
        [HttpGet]
        [Route("getPrenotazioniByDate")]
        public IActionResult GetPrenotazioniByDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.Info("Giorno inserite: ");
                _logger.Info("Id Stanza: " + idStanza);
                _logger.Info("StartDate: " + startDate.ToString());
                _logger.Info("EndDate: " + endDate.ToString());
                _logger.Info("Trovando tutte le prenotazioni di una data...");
                List<Prenotazione> prenotazioni = _prenotazioneService.GetAllPrenotazioniByIdStanzaAndDate(idStanza, startDate, endDate);
                _logger.Info("Prenotazioni della stanza trovate con successo");
                return Ok(prenotazioni);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        

        /// <summary>
        /// Salva una prenotazione nel database
        /// </summary>
        /// <param name="prenotazioneDto">L'oggetto dto da mappare e poi salvare</param>
        /// <returns>status 200</returns>
        [HttpPost]
        [Route("addPrenotazione")]
        public IActionResult AddPrenotazione([FromBody] PrenotazioneDto prenotazioneDto)
        {
            try
            {
                _logger.Info("Utente: " + prenotazioneDto.Utente.Nome);
                _logger.Info("Inizio data: " + prenotazioneDto.StartDate.ToString());
                _logger.Info("Fine data: " + prenotazioneDto.EndDate.ToString());
                _logger.Info("Stanza: " + prenotazioneDto.Stanza.Nome);
                _logger.Info("Aggiungendo una prenotazioneDto nel database...");
                _prenotazioneService.Save(prenotazioneDto);
                _logger.Info("PrenotazioneDto aggiunto con successo!");
                return Ok();
            }
            catch(ArgumentException ex)
            {
                _logger.Error("Errore insertimento del parametro: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Error("Errore: " + ex.Message);
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
