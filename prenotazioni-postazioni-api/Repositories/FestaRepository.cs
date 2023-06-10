using prenotazione_postazioni_libs.Models;
using Newtonsoft.Json;
using prenotazioni_postazioni_api.Repositories.Database;
using prenotazioni_postazioni_api.Services;
using log4net;
using System.Data.SqlClient;

namespace prenotazioni_postazioni_api.Repositories
{
    public class FestaRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(FestaRepository));




        /// <summary>
        /// query al db, restituisce tutte le festa in una data
        /// </summary>
        /// <param name="date">la data</param>
        /// <returns>Lista di Feste</returns>
        internal Festa FindByDate(DateTime date)
        {
            string query = "SELECT * FROM Feste WHERE giorno = @giorno;";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@giorno", date.ToString("yyyy-MM-dd hh:mm:ss:fff"));
            return DatabaseManager<Festa>.GetInstance().MakeQueryOneResult(sqlCommand);
        }
        /// <summary>
        /// query al db, restituisce tutte le festa
        /// </summary>
        /// <returns>Lista di festa</returns>
        internal List<Festa> FindAll()
        {
            string query = $"SELECT * FROM Feste";
            SqlCommand sqlCommand = new SqlCommand(query);
            return DatabaseManager<List<Festa>>.GetInstance().MakeQueryMoreResults(sqlCommand);
        }

        /// <summary>
        /// query al db, salva una festa al database
        /// </summary>
        /// <param name="festa">la festa da salvare</param>
        internal void Save(Festa festa)
        {
            string query = $"INSERT INTO Feste (giorno, descrizione) VALUES (@festa_giorno, @festa_desc);";
            SqlCommand sqlCommand = new SqlCommand(query);
            sqlCommand.Parameters.AddWithValue("@festa_giorno", festa.Giorno.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCommand.Parameters.AddWithValue("@festa_desc", festa.Descrizione);
            DatabaseManager<object>.GetInstance().MakeQueryNoResult(sqlCommand);
        }
    }
}
