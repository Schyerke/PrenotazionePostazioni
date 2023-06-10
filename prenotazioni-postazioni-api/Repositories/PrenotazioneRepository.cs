using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class PrenotazioneRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(PrenotazioneRepository));

        public PrenotazioneRepository() { }

        /// <summary>
        /// Query al db per restitire una Prenotazione in base al suo Id
        /// </summary>
        /// <param name="idPrenotazione">Id della Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal Prenotazione FindById(int idPrenotazione)
        {

            string query = "SELECT * FROM Prenotazioni WHERE idPrenotazioni = @id_prenotazione;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_prenotazione", idPrenotazione);
            return DatabaseManager<Prenotazione>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id della stanza associata
        /// </summary>
        /// <param name="idStanza">L'Id della stanza associata alla Prenotazione</param>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindByStanza(int idStanza)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idStanza = @id_stanza;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_stanza", idStanza);
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Query al db, per trovare tutte le prenotazioni esistenti al database
        /// </summary>
        /// <returns>Lista di Prenotazione</returns>
        internal List<Prenotazione> FindAll()
        {
            string query = "SELECT * FROM Prenotazioni;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        /// <summary>
        /// Query al db per restituire una Prenotazione in base all'id dell'utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente associata alla Prenotazione</param>
        /// <returns>Prenotazione</returns>
        internal List<Prenotazione> FindByUtente(int idUtente)
        {
            string query = $"SELECT * FROM Prenotazioni WHERE idUtente = @id_utente;";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id_utente", idUtente);
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(cmd);

        }

        /// <summary>
        /// Query al db, aggiunge una nuova prenotazione nella tabella Prenotazioni
        /// </summary>
        /// <param name="prenotazione">La prenotazione da aggiungere al database</param>
        internal void Save(Prenotazione prenotazione)
        {
            string query = $"INSERT INTO Prenotazioni (startDate, endDate, idStanza, idUtente) VALUES (@start_date, @end_date, @id_stanza, @id_utente);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@start_date", prenotazione.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@end_date", prenotazione.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@id_utente", prenotazione.IdUtente);
            sqlCommand.Parameters.AddWithValue("@id_stanza", prenotazione.IdStanza);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }


        /// <summary>
        /// Query al db, restituisce una lista di prenotazione mediante la stanza e l'intervallo di tempo
        /// </summary>
        /// <param name="idStanza">L'id della stanza dove sono effettuate delle prenotazioni</param>
        /// <param name="date">La data delle prenotazioni</param>
        /// <returns></returns>
        internal List<Prenotazione> FindAllByIdStanzaAndDate(int idStanza, DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string end = endDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string query = $"SELECT * FROM dbo.Prenotazioni WHERE((idStanza = @id_stanza)AND((startDate BETWEEN @start AND @end) OR(endDate BETWEEN @start AND @end) OR (DATEDIFF(HH, startDate, @start) >= 0 AND DATEDIFF(HH, endDate, @end) <= 0)))";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_stanza", idStanza);
            sqlCommand.Parameters.AddWithValue("@start", start);
            sqlCommand.Parameters.AddWithValue("@end", end);
            return DatabaseManager<List<Prenotazione>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }





    }
}
