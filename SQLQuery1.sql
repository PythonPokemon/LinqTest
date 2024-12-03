/*
-- Erstellen der Datenbank
CREATE DATABASE UniversitaetDB;

-- Wechseln in die neue Datenbank
USE UniversitaetDB;

-- Erstellen der Tabelle 'Universitaeten'
CREATE TABLE Universitaeten (
    UniversitaetID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Standort NVARCHAR(100)
);

-- Erstellen der Tabelle 'Personen'
CREATE TABLE Personen (
    PersonID INT PRIMARY KEY IDENTITY(1,1),
    Vorname NVARCHAR(50),
    Nachname NVARCHAR(50),
    UniversitaetID INT,
    FOREIGN KEY (UniversitaetID) REFERENCES Universitaeten(UniversitaetID)
);

-- Einfügen von Testdaten in 'Universitaeten'
INSERT INTO Universitaeten (Name, Standort) VALUES 
('Technische Universität München', 'München'),
('Universität Heidelberg', 'Heidelberg');

-- Einfügen von Testdaten in 'Personen'
INSERT INTO Personen (Vorname, Nachname, UniversitaetID) VALUES
('Max', 'Mustermann', 1),
('Erika', 'Musterfrau', 2);


INSERT INTO Universitaeten (Name, Standort)
VALUES ('Technische Universität', 'Berlin'),
       ('Universität München', 'München');

INSERT INTO Personen (Vorname, Nachname, UniversitaetID)
VALUES ('Max', 'Mustermann', 1),  -- Person für die Universität 'Technische Universität'
       ('Anna', 'Beispiel', 2);    -- Person für die Universität 'Universität München'

*/



select * from Personen;
select * from Universitaeten;