namespace prenotazione_postazioni_mvc.Models
{
    public class FestivitaImpostazioniViewModel
    {

        public DateTime GiornoSelezionato { get; set; }
        public List<DateTime> Festivita { get; set; }

        public FestivitaImpostazioniViewModel(DateTime giornoSelezionato, List<DateTime> festivita)
        {
            GiornoSelezionato = giornoSelezionato;
            Festivita = festivita;
        }

        public FestivitaImpostazioniViewModel(List<DateTime> festivita)
        {
            Festivita = festivita;
        }

        public FestivitaImpostazioniViewModel()
        {
            Festivita = new List<DateTime>();
            //Query API che prende le feste dal db
        }

        public void AddFesta(DateTime giorno)
        {
            if (Festivita.Contains(giorno))
                throw new Exception("Festività già inserita");

            Festivita.Add(giorno);
        }

        public void RemoveFesta(DateTime giorno)
        {
            if (!Festivita.Contains(giorno))
                throw new Exception("Festività non presente");

            Festivita.Remove(giorno);
        }

        public string GetFestaSelezionata()
        {
            return GiornoSelezionato.Year == 1 ? "Seleziona un giorno" : "Giorno selezionato: "+GiornoSelezionato.Day+"/"+GiornoSelezionato.Month+"/"+GiornoSelezionato.Year;
        }
    }
}
