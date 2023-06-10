using prenotazione_postazioni_libs.Dto;
using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class VotoService
    {
        private VotoRepository _votoRepository;
        private UtenteService _utenteService;
        private readonly ILog logger = LogManager.GetLogger(typeof(VotoService));
        public VotoService(VotoRepository votoRepository, UtenteService utenteService)
        {
            _votoRepository = votoRepository;
            _utenteService = utenteService;
        }



        /// <summary>
        /// Serve a ottenere tutti i voti fatti da un utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiFromUtente(int idUtente)
        {
            logger.Info("Trovando tutti i voti effettuati da un utente...");
            logger.Info($"Verifico che l'idUtente {idUtente} sia associato a un utente valido");
            Utente utenteApp = _utenteService.GetUtenteById(idUtente);
            return _votoRepository.FindAllByIdUtenteFrom(idUtente);
        }

        /// <summary>
        /// Serve a ottenere tutti i voti che sono stati fatti verso un determinato utente
        /// </summary>
        /// <param name="idUtente"></param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> GetVotiToUtente(int idUtente)
        {
            logger.Info("Trovando tutti  i voti effettuati verso un determinato utente...");
            logger.Info($"Verifico che l'idUtente {idUtente} sia associato a un utente valido");
            Utente utenteApp = _utenteService.GetUtenteById(idUtente);
            return _votoRepository.FindAllByIdUtenteTo(idUtente);
        }

        /// <summary>
        /// Aggiunge un voto al database 
        /// </summary>
        /// <param name="votoDto"></param>
        internal void MakeVotoToUtente(VotoDto votoDto)
        {
            logger.Info("Trovando il voto mediante l'id dell'utente " + votoDto.Utente.IdUtente + " che ha votato e l'id dell'utente " + votoDto.UtenteVotato.IdUtente + " che ha ricevuto il voto");
            Voto voto = _votoRepository.FindByIdUtenteToAndIdUtenteFrom(votoDto.Utente.IdUtente, votoDto.UtenteVotato.IdUtente);
            logger.Info("Controllando se il voto e' null..");
            if (voto == null)
            {
                logger.Info("Il voto e' null, e' valido");
                logger.Info("Convertendo il votoDto in Voto...");
                logger.Info("Procedo con il salvataggio nel database");
                _votoRepository.Save(new Voto(votoDto.Utente.IdUtente, votoDto.UtenteVotato.IdUtente, votoDto.VotoEffettuato));
                return;
            }
            logger.Info("Il voto non e' null, il voto dunque e' gia stato effettuato in precedenza");
            logger.Info("Procedo con il cambiare il voto effettuato...");
            logger.Info("Controllo se il voto effettutato e' uguale al voto nel votoDto...");
            if(voto.VotoEffettuato == votoDto.VotoEffettuato)
            {
                logger.Fatal("ERRORE: Il voto effettutato e' uguale a quello nel votoDto...");
                throw new PrenotazionePostazioniApiException("Il voto e' uguale");
            }
            logger.Info("Aggiorno il voto!");
            //switch il valore del voto
            _votoRepository.UpdateVoto(voto);
        }

        internal void DeleteVoto(int idUtente, int idUtenteVotato)
        {
            bool ok = false;
            int id = 0;
            logger.Info($"Eliminazione voto di {idUtente} verso {idUtenteVotato}");
            logger.Info($"Ricerco esistenza voto");
            List<Voto> votiApp = GetVotiFromUtente(idUtente);
            foreach(Voto voto in votiApp) if (!ok)
            {
                if(voto.IdUtenteVotato == idUtenteVotato)
                {
                    ok = true;
                    id = voto.IdVoto;
                }
            }
            if(ok == true)
            {
                logger.Info("Voto da eliminare trovato!");
                logger.Info("Elimino il voto...");
                _votoRepository.DeleteVoto(id);
            }else
            {
                logger.Fatal("ERRORE: Voto da elimare non valido!");
                throw new PrenotazionePostazioniApiException("Voto da eliminare non esistente");
            }
        }
    }
}
