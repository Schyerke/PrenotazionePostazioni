using prenotazioni_postazioni_api.Repositories;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Services
{
    public class ImpostazioneService
    {
        private ImpostazioneRepository _impostazioneRepository;
        private readonly ILog logger = LogManager.GetLogger(typeof(ImpostazioneService));

        public ImpostazioneService(ImpostazioneRepository impostazioneRepository)
        {
            this._impostazioneRepository = impostazioneRepository;
        }

        /// <summary>
        /// Restituisce il valore Impostazione Emergenza situato nella tabella Impostazioni nel database.
        /// </summary>
        /// <returns>Valore effettivo dell'impostazione di emergenza. True, o False</returns>
        /// <exception cref="PrenotazionePostazioniApiException"></exception>
        public bool GetImpostazioneEmergenza()
        {
            logger.Info("Chiamando FindImpostazioneEmergenza() per trovare l'impostazione di emergenza...");
            Impostazioni impostazioni = _impostazioneRepository.FindImpostazioneEmergenza();
            logger.Info("Controllando se impostazioni (Risultato trovato) e' valida...");
            if (impostazioni == null)
            {
                logger.Warn("Impostazioni non e' valida! Ho lanciato una PrenotazionePostazioniApiException!");
                throw new PrenotazionePostazioniApiException("Impostazione di emergenza non trovata");
            }
            else
            {
                logger.Info("Impostazioni e' valida!");
                return impostazioni.ModEmergenza;

            }
        }

        /// <summary>
        /// Aggiorna il campo di Impostazioni Emergenza nel Database con il valore inserito nel primo parametro
        /// </summary>
        /// <param name="userValue">Il valore con cui si aggiornera Impostazioni Emergenza</param>
        /// <returns>Lo stato di Impostazione Emergenza aggiornata</returns>
        public void ChangeImpostazioniEmergenza()
        {
            logger.Info("Controllando se Impostazioni Emergenza e' a true...");
            if (GetImpostazioneEmergenza() == true)
            {
                logger.Info("Impostazione emergenza e' a true! L'ho cambiato a false!");
                _impostazioneRepository.UpdateImpostazioneEmergenza(false);
            }
            else
            {
                logger.Info("Impostazione emergenza e' a false! L'ho cambiato a true!");
                _impostazioneRepository.UpdateImpostazioneEmergenza(true);
            }
        }
        
    }
}
