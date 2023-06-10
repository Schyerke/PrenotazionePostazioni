using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-Image-to-base64-in-c-sharp)
        public string Email { get; set; }
        public int IdRuolo { get; set; }


        private Exception ModelException { get; set; }
        public bool IsValid { get; set; } = false;

        public Utente(int idUtente, string nome, string cognome, string image, string email, int idRuolo)
        {
            this.IdUtente = idUtente;
            this.Nome = nome;
            this.Cognome = cognome;
            this.Image = image;
            this.Email = email;
            this.IdRuolo = idRuolo;

            this.Validate();
        }



        public Utente(string nome, string cognome, string image, string email, int idRuolo)
        {
            Nome = nome;
            Cognome = cognome;
            Image = image;
            Email = email;
            IdRuolo = idRuolo;
            this.Validate();
        }

        public Utente()
        {
        }

        public void Validate()
        {
            try
            {
                if (this.Nome == null)
                    throw new Exception("Il Nome non può essere nullo");
                else if (this.Nome.Length < 3)
                    throw new Exception("Il Nome deve contenere almeno 3 caratteri");

                if (this.Cognome == null)
                    throw new Exception("Il Cognome non può essere nullo");
                else if (this.Cognome.Length < 3)
                    throw new Exception("Il Cognome deve contenere almeno 3 caratteri");

                if (this.Email == null)
                    throw new Exception("L'indirizzo Email non può essere nullo");
                else if (!this.Email.Contains("@"))
                    throw new Exception("L'indirizzo Email non è valido");


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
