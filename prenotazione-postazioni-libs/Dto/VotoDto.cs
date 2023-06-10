using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class VotoDto
    {
        public Utente Utente { get; set; }
        public Utente UtenteVotato { get; set; }
        public bool VotoEffettuato { get; set; }

        public VotoDto(Utente utente, Utente utenteVotato, bool votoEffettuato)
        {
            this.Utente = utente;
            this.UtenteVotato = utenteVotato;
            this.VotoEffettuato = votoEffettuato;
        }

        public VotoDto()
        {
        }
    }
}
