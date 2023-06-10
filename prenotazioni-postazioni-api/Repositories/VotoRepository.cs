using prenotazione_postazioni_libs.Dto;
using Newtonsoft.Json;
using prenotazione_postazioni_libs.Models;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace prenotazioni_postazioni_api.Repositories
{
    public class VotoRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(VotoRepository));

        public VotoRepository()
        {
        }



        /// <summary>
        /// Query al db, restituisce tutti i voti dell'utente che ha votato
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteFrom(int idUtente)
        {
            string query = $"SELECT * FROM Voti WHERE idUtente = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            return DatabaseManager<List<Voto>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce tutti i voti fatti ad un utente
        /// </summary>
        /// <param name="idUtente">L'id dell'utente</param>
        /// <returns>Lista di voti</returns>
        internal List<Voto> FindAllByIdUtenteTo(int idUtente)
        {
            string query = $"SELECT * FROM voti WHERE idUtenteVotato = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            return DatabaseManager<List<Voto>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// query al db, salva un voto al database
        /// </summary>
        /// <param name="voto">il voto che verra salvato nel database</param>
        internal void Save(Voto voto)
        {
            string query = $"INSERT INTO Voti (idUtente, idUtenteVotato, votoEffettuato) VALUES (@id_utente, @id_utente_votato, @voto_effettutato);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", voto.IdUtente);
            sqlCommand.Parameters.AddWithValue("@id_utente_votato", voto.IdUtenteVotato);
            sqlCommand.Parameters.AddWithValue("@voto_effettutato", voto.VotoEffettuato);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
        /// <summary>
        /// query al db, restituisce il voto che un utente ha effettuato ad un altro utente
        /// </summary>
        /// <param name="idUtente">L'utente che ha votato</param>
        /// <param name="idUtenteVotato">L'utente che e' stato votato</param>
        /// <returns>Il voto che trovato, null altrimenti</returns>
        internal Voto? FindByIdUtenteToAndIdUtenteFrom(int idUtente, int idUtenteVotato)
        {
            string query = $"SELECT * FROM Voti WHERE idUtente = @id_utente AND idUtenteVotato = @id_utente_votato;";
            SqlCommand sqlCommmand = new SqlCommand(query);
            sqlCommmand.Parameters.AddWithValue("@id_utente", idUtente);
            sqlCommmand.Parameters.AddWithValue("@id_utente_votato", idUtenteVotato);
            return DatabaseManager<Voto>.GetInstance().MakeQueryOneResult(sqlCommmand);
        }

        /// <summary>
        /// query al db, aggiorna il voto al suo opposto
        /// </summary>
        /// <param name="voto">Il voto da aggiornare</param>
        internal void UpdateVoto(Voto voto)
        {
            string query = $"UPDATE Voti SET votoEffettuato = 1 ^ voto WHERE idUtente = @id_utente AND idUtenteVotato = @id_utente_votato;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", voto.IdUtente);
            sqlCommand.Parameters.AddWithValue("@id_utente_votato", voto.IdUtenteVotato);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal void DeleteVoto(int idVoto)
        {
            string query = $"DELETE FROM Voti WHERE idVoto = @id_voto;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_voto", idVoto);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
