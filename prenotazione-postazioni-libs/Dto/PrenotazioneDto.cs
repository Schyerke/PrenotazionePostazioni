
using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class PrenotazioneDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Stanza Stanza { get; set; }
        public Utente Utente { get; set; }

        public PrenotazioneDto(DateTime startDate, DateTime endDate, Stanza stanza, Utente utente)
        {
            StartDate = startDate;
            EndDate = endDate;
            Stanza = stanza;
            Utente = utente;
        }

        public PrenotazioneDto()
        {
        }
    }
}
