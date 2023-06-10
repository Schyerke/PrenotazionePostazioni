
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prenotazione_postazioni_libs.Models
{
    public class Impostazioni
    {
        private static Impostazioni _instance;
        private Impostazioni()
        {

        }

        public static Impostazioni GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Impostazioni();
            }
            return _instance;
        }
    
        public bool ModEmergenza { get; set; }

    }
}