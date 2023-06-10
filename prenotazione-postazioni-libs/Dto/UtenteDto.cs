using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class UtenteDto
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Image { get; set; }
        //Base 64x converter (https://stackoverflow.com/questions/69303512/converting-an-sql-Image-to-base64-in-c-sharp)
        public string Email { get; set; }
        public Ruolo Ruolo { get; set; }
        public UtenteDto(string nome, string cognome, string image, string email, Ruolo ruolo)
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Image = image;
            this.Email = email;
            this.Ruolo = ruolo;
        }

        public UtenteDto()
        {
        }
    }




}
