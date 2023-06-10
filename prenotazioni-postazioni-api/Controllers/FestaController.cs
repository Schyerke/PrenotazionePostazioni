using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Services;

namespace prenotazioni_postazioni_api.Controllers
{
    [ApiController]
    [Route("/api/festivita")]
    public class FestaController : ControllerBase
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(FestaController));
        private FestaService _festaService;

        public FestaController(FestaService festaService)
        {
            this._festaService = festaService;
        }





        /// <summary>
        /// Restituisce tutte le feste di un giorno
        /// </summary>
        /// <param name="date">Il giorno </param>
        /// <returns>Lista di feste trovate</returns>

        [Route("getByDate")]
        [HttpGet]
        public IActionResult GetByDate(int year, int month, int day)
        {
            try
            {
                logger.Info($"Year: {year}");
                logger.Info("Month: " + month);
                logger.Info("Day: " + day);
                logger.Info("Trovando una festa mediante date...");
                Festa festa = _festaService.GetByDate(new DateTime(year, month, day));
                if(festa == null)
                {
                    logger.Warn("Festa e' null, return NotFound");
                    return NotFound("Festa è null");
                }
                logger.Info("Festa trovato. Return OK");
                return Ok(festa);
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore interno: " + ex.Message);
                return StatusCode(500, ex.Message+"\nStack Trace:"+ex.StackTrace);
            }
            
        }
        /// <summary>
        /// Restituisce tutte le feste fatte
        /// </summary>
        /// <returns>Lista di tutte le feste trovate</returns>
        [Route("getAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                logger.Info("Trovando tutte le feste...");
                List<Festa> feste = _festaService.GetAll();
                if(feste == null)
                {
                    logger.Warn("Nessuna festa trovata, NotFound");
                    return NotFound("feste e' null");
                }
                logger.Info("Feste trovate, Ok");
                return Ok(feste);
            }
            catch (PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("addFesta")]
        [HttpPost]
        public IActionResult AddFestaByDate([FromBody] FestaDto festaDto)
        {
            try
            {
                logger.Info("Giorno della festa: " + festaDto.Date);
                logger.Info("Descrizione della festa: " + festaDto.Desc);
                logger.Info("Salvando una festaDto del database...");
                _festaService.Save(festaDto);
                logger.Info("FestaDto salvato con successo, Ok");
                return Ok();
            }
            catch(PrenotazionePostazioniApiException ex)
            {
                logger.Error("Bad request: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.Fatal("Errore Interno: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        //[Route("deleteFestaByDate")]
    }
}
