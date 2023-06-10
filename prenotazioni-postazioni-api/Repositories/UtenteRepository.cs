using prenotazione_postazioni_libs.Dto;
using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class UtenteRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(UtenteRepository));

        public UtenteRepository()
        {
        }



        /// <summary>
        /// Serve per ottenere una lista completa di tutti gli utenti
        /// </summary>
        /// <returns>Lista di Utente trovati, null altrimenti</returns>
        internal List<Utente> FindAll()
        {
            string query = "SELECT * FROM Utenti;";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Utente>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
        

        /// <summary>
        /// Query al db, restituisce un utente mediante il suo id
        /// </summary>
        /// <param name="id">L'id dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindById(int idUtente)
        {
            string query = $"SELECT * FROM Utenti WHERE idUtente = @id_utente;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@id_utente", idUtente);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al db, restituisce un utente mediante la sua email
        /// </summary>
        /// <param name="email">L'email dell'utente da trovare</param>
        /// <returns>L'utente trovato, null altrimenti</returns>
        internal Utente FindByEmail(string email)
        {
            string query = $"SELECT * FROM Utenti WHERE email = @email;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@email", email);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }


        /// <summary>
        /// Query al db, salva un utente al database
        /// </summary>
        /// <param name="utente">L'utente che verra salvato nel database (tabella Utenti)</param>
        internal void Save(Utente utente)
        {
            string query = $"INSERT INTO Utenti (nome, cognome, immagine, email, idRuolo) VALUES (@nome, @cognome, @image, @email, @id_ruolo);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@nome", utente.Nome);
            sqlCommand.Parameters.AddWithValue("@cognome", utente.Cognome);
            sqlCommand.Parameters.AddWithValue("@image", utente.Image);
            sqlCommand.Parameters.AddWithValue("@email", utente.Email);
            sqlCommand.Parameters.AddWithValue("@id_ruolo", utente.IdRuolo);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }

        internal Utente FindByName(string nome, string cognome)
        {
            string query = $"SELECT * FROM Utenti WHERE (nome = @nome AND cognome = @cognome)";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@nome", nome);
            sqlCommand.Parameters.AddWithValue("@cognome", cognome);
            return DatabaseManager<Utente>.GetInstance().MakeQueryOneResult(sqlCommand);
        }

        /// <summary>
        /// Query al Db, restituisce gli id degli utenti che hanno prenotato una postazione in un dato giorno
        /// </summary>
        /// <param name="date"></param>
        /// <returns>List di Utente contenenti solo gli Id</returns>
        internal List<Utente> FindUtentiByDate(DateTime date)
        {
            string query = $"SELECT idUtente FROM Prenotazioni WHERE YEAR(startDate) = @year AND MONTH(startDate) = @month AND DAY(startDate) = {date.Day};";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@year", date.Year);
            sqlCommand.Parameters.AddWithValue("@month",date.Month);
            sqlCommand.Parameters.AddWithValue("@day", date.Day);
            return DatabaseManager<List<Utente>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }
    }
}