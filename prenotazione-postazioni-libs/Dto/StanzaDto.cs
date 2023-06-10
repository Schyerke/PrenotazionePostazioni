
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class StanzaDto
    {
        public string Nome { get; set; }
        public int PostiMax { get; set; }
        public int PostiMaxEmergenza { get; set; }

        public StanzaDto(string nome, int postiMax, int postiMaxEmergenza)
        {
            this.Nome = nome;
            this.PostiMax = postiMax;
            this.PostiMaxEmergenza = postiMaxEmergenza;
        }

        public StanzaDto()
        {
        }
    }
}
