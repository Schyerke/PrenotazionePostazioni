using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Festa
    {
        public int IdFesta { get; set; }
        public DateTime Giorno { get; set; }
        public string? Descrizione { get; set; }

        public Exception ModelException;
        public bool IsValid { get; set; } = false;

        public Festa()
        {

        }
        public Festa(DateTime date, string? desc)
        {
            this.Giorno = date;
            this.Descrizione = desc;
            this.Validate();
        }
        public Festa(int idFestivita, DateTime date, string? desc)
        {
            IdFesta = idFestivita;
            Giorno = date;
            Descrizione = desc;
            this.Validate();
        }
        public Festa(DateTime date)
        {
            this.Giorno = date;
            this.Validate();
        }


        private void Validate()
        {
            try
            {
                if (this.Giorno == null)
                    throw new Exception("Giorno non puo essere null");
                this.IsValid = true;
            }
            catch (Exception ex)
            {
                this.IsValid = false;
                this.ModelException = ex;
            }

        }
    }
}
