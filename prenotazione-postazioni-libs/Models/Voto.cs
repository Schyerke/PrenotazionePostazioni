using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Voto
    {

        public int IdVoto { get; set; }
        public int IdUtente { get; set; }
        public int IdUtenteVotato { get; set; }
        public bool VotoEffettuato { get; set; }

        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public Voto(int idVoto, int idUtente, int idUtenteVotato, bool votoEffettuato)
        {
            this.IdVoto = idVoto;
            this.IdUtente = idUtente;
            this.IdUtenteVotato = idUtenteVotato;
            this.VotoEffettuato = votoEffettuato;

            this.Validate();
        }

        public Voto(int idUtente, int idUtenteVotato, bool votoEffettuato)
        {
            IdUtente = idUtente;
            IdUtenteVotato = idUtenteVotato;
            VotoEffettuato = votoEffettuato;
            this.Validate();
        }

        public Voto()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.IdUtente == this.IdUtenteVotato)
                    throw new Exception("L'id dell'idUtente votato non può essere lo stesso dell'idUtente che vota");

                this.IsValid = true;
            }
            catch (Exception e)
            {
                this.ModelException = e;
                this.IsValid = false;
            }
        }
    }
}
