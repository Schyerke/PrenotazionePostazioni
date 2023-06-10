using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Dto
{
    public class FestaDto
    {
        public DateTime Date { get; set; }
        public string? Desc { get; set; }
        
        public FestaDto()
        {

        }
        public FestaDto(DateTime date, string? desc)
        {
            Date = date;
            Desc = desc;
        }
        public FestaDto(DateTime date)
        {
            this.Date = date;   
        }
    }
}
