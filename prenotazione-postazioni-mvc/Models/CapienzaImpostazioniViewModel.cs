namespace prenotazione_postazioni_mvc.Models
{
    public class CapienzaImpostazioniViewModel
    {

        public string Stanza { get; set; }
        public int Capienza { get; set; }

        
        public CapienzaImpostazioniViewModel(string stanza, int capienza)
        {
            Stanza = stanza;
            Capienza = capienza;
        }

        public CapienzaImpostazioniViewModel(string stanza)
        {
            Stanza = stanza;
            Capienza = 5;
        }

        public CapienzaImpostazioniViewModel()
        {
            Capienza = 5;
        }

        public void Validate()
        {
            if (Stanza == null)
                throw new Exception("Seleziona una stanza");

            if (Capienza <= 0)
                throw new Exception("Capienza non valida");
        }

        public string GetStanza()
        {
            return Stanza == null ? "Seleziona una stanza" : Stanza;
        }
    }
}
