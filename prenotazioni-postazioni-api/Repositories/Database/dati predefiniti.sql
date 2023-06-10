INSERT INTO Impostazioni
VALUES (0);

INSERT INTO Ruoli (idRuolo,descRuolo,accessoImpostazioni)
VALUES (1,'Admin',1);
INSERT INTO Ruoli (idRuolo,descRuolo,accessoImpostazioni)
VALUES (2,'User',0);

INSERT INTO Utenti (nome,cognome,email,idRuolo)
VALUES ('Manuel','Mauro','manuelmauro04@gmail.com',1);
INSERT INTO Utenti (nome,cognome,email,idRuolo)
VALUES ('Stefano','Hu','stefano.hu1.stud@tulliobuzi.edu.it',2);
INSERT INTO Utenti (nome,cognome,email,idRuolo)
VALUES ('Arianna','Bustone','arianna.bustone@gmail.com',2);
INSERT INTO Utenti (nome,cognome,email,idRuolo)
VALUES ('Andrea','Redegalli','reedegalli03@gmail.com',2);

INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Meeting',12,8);
INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Open space 1',10,6);
INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Open space 2',10,6);
INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Helpdesk',10,6);
INSERT INTO Stanze (nome,postiMax,postiMaxEmergenza)
VALUES ('Development',14,8);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T12:00:00',1,1);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T12:00:00','2022-07-14T13:00:00',2,1);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T14:00:00','2022-07-14T17:00:00',5,1);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T17:00:00','2022-07-14T18:00:00',3,1);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T13:00:00',5,2);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T14:00:00','2022-07-14T18:00:00',5,2);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T10:00:00',1,3);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T10:00:00','2022-07-14T13:00:00',5,3);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T14:00:00','2022-07-14T18:00:00',3,3);

INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T09:00:00','2022-07-14T13:00:00',2,4);
INSERT INTO Prenotazioni (startDate,endDate,idStanza,idUtente)
VALUES ('2022-07-14T14:00:00','2022-07-14T18:00:00',1,4);

INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (1,2,1);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (1,3,0);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (2,1,1);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (3,2,1);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (3,4,0);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (4,2,1);
INSERT INTO Voti (idUtente,idUtenteVotato,votoEffettuato)
VALUES (4,1,0);

INSERT INTO Feste (giorno, descrizione)
VALUES ('2022-12-25','Natale');
INSERT INTO Feste (giorno, descrizione)
VALUES ('2022-12-31','Ultimo dell anno');