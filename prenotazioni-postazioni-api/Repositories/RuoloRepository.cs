using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using System.Data.SqlClient;
using prenotazioni_postazioni_api.Exceptions;
using log4net;

namespace prenotazioni_postazioni_api.Repositories
{
    public class RuoloRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(RuoloRepository));

        public RuoloRepository()
        {
        }

        /// <summary>
        /// Query al db, restituisce il ruolo dell'utente associato usando il suo ID
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Ruolo trovato, null altrimenti</returns>
        public Ruolo? FindById(int idRuolo)
        {
            string query = $"SELECT * FROM Ruoli WHERE idRuolo = {idRuolo};";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<Ruolo>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce il ruolo di un utente mediante il suo id
        /// </summary>
        /// <param name="idUtente">L'id dell'utente che servira per trovare il suo ruolo</param>
        /// <returns>Ruolo dell'utente, null altrimenti</returns>
        public Ruolo? FindByIdUtente(int idUtente)
        {
            string query = $"SELECT * FROM Utenti WHERE idUtente = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            Utente utente = DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
            if(utente == null)
            {
                throw new PrenotazionePostazioniApiException("IdUtente non trovato");
            }
            query = $"SELECT * FROM Ruoli WHERE idRuolo = @id_ruolo;";
            sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_ruolo", utente.IdRuolo);
            return DatabaseManager<Ruolo>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db, switch il ruolo accesso impostazioni dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente che gli verra cambiato il ruolo</param>
        /// <param name="ruoloEnum">Il ruolo con cui verra aggiornato l'utente</param>
        internal void UpdateRuolo(int idUtente, RuoloEnum ruoloEnum)
        {
            string query = $"UPDATE Utenti SET idRuolo = @ruolo_enum WHERE idUtente = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@ruolo_enum", ruoloEnum.ToString());
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
