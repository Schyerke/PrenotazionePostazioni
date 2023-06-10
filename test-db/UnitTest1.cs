using Microsoft.VisualStudio.TestTools.UnitTesting;
using prenotazione_postazioni_libs.Models;
using System;
using System.Collections.Generic;

namespace test_db
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int MAX_STANZA = 3;
            List<Prenotazione> prenotazioneMock = new List<Prenotazione>(){
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 9, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 10, 0, 0),
                    IdStanza = 1,
                    IdUtente = 1
                },
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 9, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 12, 0, 0),
                    IdStanza = 1,
                    IdUtente = 2
                },
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 10, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 13, 0, 0),
                    IdStanza = 1,
                    IdUtente = 3
                },
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 14, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 16, 0, 0),
                    IdStanza = 1,
                    IdUtente = 4
                },
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 15, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 18, 0, 0),
                    IdStanza = 1,
                    IdUtente = 5
                },
                new Prenotazione
                {
                    StartDate = new DateTime(2022, 7, 11, 11, 0, 0),
                    EndDate = new DateTime(2022, 7, 11, 13, 0, 0),
                    IdStanza = 1,
                    IdUtente = 10
                }
            };

            Prenotazione newPrenotazione = new Prenotazione(new DateTime(2022, 7, 11, 9, 0, 0), new DateTime(2022, 7, 11, 18, 0, 0), 1, 8);


            //implementazione dell'algoritmo
            int contatore = 1;
            int checkContatore = 1;
            int maxOre = 1;
            for (int i = newPrenotazione.StartDate.Hour; i <= newPrenotazione.EndDate.Hour; i++)
            {
                contatore = 1;
                checkContatore = 1;
                foreach (var prenotazione in prenotazioneMock)
                {
                    if (prenotazione.StartDate.Hour <= i && i < prenotazione.EndDate.Hour)
                    {
                        contatore++;
                    }
                    checkContatore++;
                    if (contatore > MAX_STANZA)
                    {
                        maxOre++;
                        if (contatore < checkContatore)
                        {
                            int inizioOreBlocco = contatore - maxOre - 1;
                            int fineOreBlocco = contatore;
                            //return false; 
                        }
                    }
                }
            }
            //return true;
        }
    }
}
