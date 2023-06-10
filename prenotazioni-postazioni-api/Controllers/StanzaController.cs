
 using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using Microsoft.AspNetCore.Cors;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/stanze")]
    public class StanzaController : ControllerBase
    {
        private StanzaService _stanzaService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(StanzaController));

        public StanzaController(StanzaService serviceService)
        {
            _stanzaService = serviceService;
        }
        /// <summary>
        /// Restituisce tutte le stanze
        /// </summary>
        /// <returns>Lista di tutte le stanze</returns>
        [HttpGet]
        [Route("getAllStanze")]
        public IActionResult GetAllStanze()
        {
            try
            {
                _logger.Info("Trovando tutte le stanze...");
                List<Stanza> stanze = _stanzaService.GetAllStanze();
                _logger.Info("Stanze trovate con successo!");
                return Ok(stanze);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        /// <summary>
        /// Restituisce la stanza mediante l'id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>La stanza trovata con 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getStanzeById")]
        public IActionResult GetStanzaById(int id)
        {
            try
            {
                _logger.Info("Id stanza: " + id);
                _logger.Info("Trovando la stanza mediante il suo id: " + id + "...");
                Stanza stanza = _stanzaService.GetStanzaById(id);
                _logger.Info("Stanza trovata con successo!");
                return Ok(stanza);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Restituisce la stanza mediante il suo nome
        /// </summary>
        /// <param name="stanzaName">Il nome della stanza da trovare</param>
        /// <returns>La stanza trovata e 200, 404 altrimenti </returns>
        [HttpGet]
        [Route("getStanzaByName")]
        public IActionResult GetStanzaByName(string stanzaName)
        {
            try
            {
                _logger.Info("Nome della stanza: " + stanzaName);
                _logger.Info("Trovando la stanza mediante il suo nome: " + stanzaName + "...");
                Stanza stanza = _stanzaService.GetStanzaByName(stanzaName);
                _logger.Info("Stanza trovata con successo!");
                return Ok(stanza);
            }
            catch (PrenotazionePostazioniApiException ex)
            {

                _logger.Warn("stanza non trovata: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge una nuova stanza al database
        /// </summary>
        /// <param name="stanzaDto">L'oggetto stanza da aggiungere al database</param>
        /// <returns>httpstatus 200</returns>
        [HttpPost]
        [Route("addStanza")]
        public IActionResult AddNewStanza(StanzaDto stanzaDto)
        {
            try
            {
                _logger.Info("Nome della stanza: " + stanzaDto.Nome);
                _logger.Info("Salvando una stanzaDto nel database...");
                _stanzaService.Save(stanzaDto);
                _logger.Info("StanzaDto salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("bad request: " + ex.Message);
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
