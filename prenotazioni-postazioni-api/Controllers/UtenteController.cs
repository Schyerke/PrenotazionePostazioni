using prenotazione_postazioni_libs.Dto;
using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/utenti")]
    public class UtenteController : ControllerBase
    {
        private UtenteService _utenteService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(UtenteController));

        public UtenteController( UtenteService utenteService)
        {
            _utenteService = utenteService;
        }

        [HttpGet]
        [Route("getAllUtenti")]
        public IActionResult GetAllUtenti()
        {
            try
            {
                _logger.Info("Prelevando tutti gli utenti...");
                List<Utente> utenti = _utenteService.getAllUtenti();
                _logger.Info("Prelevato tutti gli utenti con successo!");

                return Ok(utenti);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">Id dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getUtenteById")]
        public IActionResult GetUtenteById(int id)
        {
            try
            {
                _logger.Info("Id utente: " + id);
                _logger.Info("Prelevando un utente mediante il suo id: " + id + "...");
                Utente utente = _utenteService.GetUtenteById(id);
                _logger.Info("Utente trovato con successo!");
                return Ok(utente);
            }catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getUtenteByName")]
        public IActionResult GetUtenteByName(string nome, string cognome)
        {
            try
            {
                _logger.Info("Nome: " + nome);
                _logger.Info("Cognome: " + cognome);
                _logger.Info($"Trovando l'utente mediante il suo nome {nome} {cognome}...");
                Utente utente = _utenteService.GetUtenteByName(nome, cognome);
                _logger.Info($"utente trovato mediante il suo nome con successo!");
                return Ok(utente);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Restituisce un utente mediante il suo email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato e 200, 404 altrimenti</returns>
        [HttpGet]
        [Route("getUtenteByEmail")]
        public IActionResult GetUtenteByEmail(string email)
        {
            try
            {
                _logger.Info("Email: " + email);
                _logger.Info($"Trovando l'utente mediante la sua email:{email}...");
                Utente utente = _utenteService.GetUtenteByEmail(email);
                _logger.Info("Utente trovato mediante il suo email con successo!");
                return Ok(utente);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Utente non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message); 
            }
        }

        /// <summary>
        /// Dato un giorno trova l'elenco di persone che hanno effettuato una prenotazione in tale giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Lista di utenti</returns>
        [HttpGet]
        [Route("getUtentiPrenotatiByDay")]
        public IActionResult GetUtentiPrenotatiByDay(DateTime date)
        {
            try
            {
                List<Utente> utenti = _utenteService.GetUtentiPrenotatiByDay(date);
                return Ok(utenti);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore interno. " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge un utente al database
        /// </summary>
        /// <param name="utenteDto">L'utente da inserire nel database</param>
        /// <returns>httpstatus 200 se salvataggio corretto, httpstatus 400 altrimenti</returns>
        [HttpPost]
        [Route("addNewUtente")]
        public IActionResult AddNewUtente(UtenteDto utenteDto)
        {
            try
            {
                _logger.Info("Nome utente: " + utenteDto.Nome);
                _logger.Info("Cognome utente: " + utenteDto.Cognome);
                _logger.Info("Salvando un utente nel database...");
                _utenteService.Save(utenteDto);
                _logger.Info("Utente salvato nel database con successo!");
                return Ok();
            }catch(PrenotazionePostazioniApiException ex)
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
