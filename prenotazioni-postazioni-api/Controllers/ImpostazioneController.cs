using Microsoft.AspNetCore.Mvc;
using prenotazioni_postazioni_api.Repositories;
using prenotazioni_postazioni_api.Services;
using prenotazione_postazioni_libs.Models;
using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/impostazioni")]
    public class ImpostazioneController : ControllerBase
    {
        private ImpostazioneService _impostazioneService;
        private readonly ILog _logger = LogManager.GetLogger(typeof(ImpostazioneController));

        public ImpostazioneController(ImpostazioneService impostazioneService)
        {
            _impostazioneService = impostazioneService;
        }



        /// <summary>
        /// Serve per restituire lo stato dell'impostazione di emergenza
        /// </summary>
        /// <returns>Lo stato di emergenza. True o False</returns>
        [HttpGet]
        [Route("getImpostazioneEmergenza")]
        public IActionResult GetImpostazioneEmergenza()
        {
            try
            {
                _logger.Info("Prelevando l'impostazione di emergenza...");
                bool impostazineEmergenza = _impostazioneService.GetImpostazioneEmergenza();
                _logger.Info("Trovato impostazione di emergenza con successo!");
                return Ok(impostazineEmergenza);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                _logger.Warn("Impostazione non trovato: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
            
        }

        /// <summary>
        /// Serve per cambiare lo stato delll'impostazione di emergenza
        /// </summary>
        /// <param name="userValue">
        /// il valore inserito dall'utente per cambiare l'impostazione di emergenza
        /// </param>
        [HttpPost]
        [Route("changeImpostazioneEmergenza")]
        public IActionResult ChangeImpostazioneEmergenza()
        {
            try
            {
                _logger.Info("Cambiando l'impostazione di emergenza...");
                _impostazioneService.ChangeImpostazioniEmergenza();
                _logger.Info("Impostazione di emergenza cambiato con successo");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
