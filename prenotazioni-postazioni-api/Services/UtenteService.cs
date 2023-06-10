using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using prenotazioni_postazioni_api.Utilities;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class UtenteService
    {
        private UtenteRepository _utenteRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(UtenteService));

        public UtenteService(UtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }



        /// <summary>
        /// Restituisce tutti gli utenti
        /// </summary>
        /// <returns>List di Utente trovati, null altrimenti</returns>
        internal List<Utente> getAllUtenti()
        {
            logger.Info("Trovando tutti gli utenti nel database...");
            return _utenteRepository.FindAll();
        }

        /// <summary>
        /// Resituisce l'utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal Utente GetUtenteById(int id)
        {
            logger.Info("Trovando l'utente mediante il suo id: " + id);
            Utente utente = _utenteRepository.FindById(id);
            logger.Info("Controllando se l'utente trovato e' valido...");
            if (utente == null)
            {
                logger.Error("L'utente trovato NON e' valido");
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            else
            {
                logger.Info("L'utente trovato e' valido");
                return utente;

            }
        }

            /// <summary>
            /// Restituisce l'utente mediante la sua email
            /// </summary>
            /// <param name="email">L'email dell'utente da trovare</param>
            /// <returns>L'utente trovato, null altrimenti</returns>
            /// <exception cref="PrenotazionePostazioniApiException"></exception>
            internal Utente GetUtenteByEmail(string email)
        {
            logger.Info("Trovando l'utente mediante il suo email: " + email);
            Utente utente = _utenteRepository.FindByEmail(email);
            logger.Info("Controllando se l'utente e' null...");
            if (utente == null)
            {
                logger.Error("L'utente non e' valido");
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");

            }
            logger.Info("L'utente e' valido!");
            return utente;
        }

        internal Utente GetUtenteByName(string nome, string cognome)
        {
            logger.Info($"Trovando l'utente mediante il suo nome: {nome} {cognome}");
            Utente utente = _utenteRepository.FindByName(nome, cognome);
            logger.Info("Controllando se l'utente e' null...");
            if (utente == null)
            {
                logger.Error("L'utente non e' valido");
                throw new PrenotazionePostazioniApiException("Nome utente non trovato");

            }
            logger.Info("L'utente e' valido!");
            return utente;
        }


        /// <summary>
        /// Serve per salvare nel database un utente
        /// </summary>
        /// <param name="utenteDto"></param>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        internal void Save(UtenteDto utenteDto)
        {
            logger.Info("Convertendo utenteDto in Utente...");
            Utente utente = new Utente(utenteDto.Nome, utenteDto.Cognome, utenteDto.Image, utenteDto.Email, utenteDto.Ruolo.IdRuolo);
            logger.Info("Procedo con il salvataggio dell'utente nel database");
            _utenteRepository.Save(utente);
        }

        /// <summary>
        /// Dato un giorno restituisce un elenco di utenti che hanno effettuato tale prenotazione quel giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List di Utente senza duplicati</returns>
        internal List<Utente> GetUtentiPrenotatiByDay(DateTime date)
        {
            List<Utente> utentiWithDupes = _utenteRepository.FindUtentiByDate(date);
            List<Utente> utentiWithoutDupes = utentiWithDupes.Distinct(new UtenteEqualityComparer()).ToList();
            List<Utente> utenti = new List<Utente>();
            foreach(Utente utente in utentiWithoutDupes)
            {
                utenti.Add(_utenteRepository.FindById(utente.IdUtente));
            }
            return utenti;

        }

    }
}
