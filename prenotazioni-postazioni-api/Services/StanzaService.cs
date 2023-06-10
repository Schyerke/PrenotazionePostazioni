using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class StanzaService
    {
        private StanzaRepository _stanzaRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(StanzaService));
       
        public StanzaService(StanzaRepository stanzaRepository, ILogger<StanzaService> logger)
        {
            _stanzaRepository = stanzaRepository;
        }



        /// <summary>
        /// restituisce tutte le stanze presenti nel database
        /// </summary>
        /// <returns>una lista di stanza</returns>
        internal List<Stanza> GetAllStanze()
        {
            logger.Info("trovando tutte le stanze... chiamando il metodo FindAll...");
            return _stanzaRepository.FindAll();
        }

        /// <summary>
        /// restituisce una stanza mediante il suo id associato
        /// </summary>
        /// <param name="id">L'id della stanza</param>
        /// <returns>Stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetStanzaById(int id)
        {
            logger.Info("Troovando una stanza mediante il suo id: " + id);
            Stanza stanza = _stanzaRepository.FindById(id);
            logger.Info("Controllando se la stanza trovata e' valida...");
            if (stanza == null)
            {
                logger.Error("La stanza NON e' valida!");
                throw new PrenotazionePostazioniApiException("IdStanza non trovata");
            }
            logger.Info("La stanza e' valida!");
            return stanza;
        }

        /// <summary>
        /// restituisce una stanza mediante il suo nome associato
        /// </summary>
        /// <param name="stanzaName">il nome della stanza da trovare</param>
        /// <returns>stanza trovata, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Stanza GetStanzaByName(string stanzaName)
        {
            logger.Info("Trovando la stanza mediante il suo nome: " + stanzaName);
            Stanza stanza = _stanzaRepository.FindByName(stanzaName);
            logger.Info("Controllando se la stanza trovata e' valida...");
            if (stanza == null)
            {
                logger.Error("la stanza NON e' valida...");
                throw new PrenotazionePostazioniApiException("IdStanza non trovata");
            }
            else
            {
                logger.Info("La stanza e' valida!");
                return stanza;
            }
        }

        /// <summary>
        /// Salva una stanza nel database
        /// </summary>
        /// <param name="stanzaDto">la stanza da salvare</param>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal void Save(StanzaDto stanzaDto)
        {
            logger.Info("Controllando se stanzaDto e' valida...");
            if (CheckStanza(stanzaDto))
            {
                logger.Info("stanzaDto e' valida!");
                logger.Info("Convertendo la stanzaDto in Stanza...");
                Stanza stanza = new Stanza(stanzaDto.Nome, stanzaDto.PostiMax, stanzaDto.PostiMaxEmergenza);
                logger.Info("Procedo con il salvataggio della stanza nel database...");
                _stanzaRepository.Save(stanza);
            }
            else
            {
                logger.Error("La stanza NON e' valida!");
                throw new PrenotazionePostazioniApiException("IdStanza da salvare non valida");



            }
        }

            /// <summary>
            /// Controlla se esiste già una stanza con lo stesso nome di quella che si vuole inserire
            /// </summary>
            /// <param name="stanzaDto"></param>
            /// <returns>True se il nome è unico, False se la stanza è già presente</returns>
            private bool CheckStanza(StanzaDto stanzaDto)
        {
            List<Stanza> stanze = GetAllStanze();
            for(int i = 0; i < stanze.Count; i++)
            {
                if (stanze[i].Nome == stanzaDto.Nome) return false;
            }
            return true;
        }
    }
}
