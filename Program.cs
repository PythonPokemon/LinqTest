using System;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace LinqTest
{
    // Repräsentiert eine Universität
    public class Universitaet
    {
        public int UniversitaetID { get; set; }
        public string Name { get; set; }
        public string Standort { get; set; }
    }

    // Repräsentiert eine Person
    public class Person
    {
        public int PersonID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int UniversitaetID { get; set; }
        public Universitaet Universitaet { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Verbindungszeichenfolge zur SQL-Datenbank
            string connectionString = "Server=wasnezow;Database=UniversitaetDB;Trusted_Connection=True;";

            // Listen zur Speicherung der Daten
            List<Universitaet> universitaeten = new List<Universitaet>();
            List<Person> personen = new List<Person>();

            try
            {
                // Mit der Datenbank verbinden und Daten abfragen
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Verbindung zur Datenbank erfolgreich!");

                    // Abfrage für Universitäten
                    string uniQuery = "SELECT UniversitaetID, Name, Standort FROM Universitaeten";
                    using (SqlCommand command = new SqlCommand(uniQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Universitaet universitaet = new Universitaet
                            {
                                UniversitaetID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Standort = reader.GetString(2)
                            };
                            universitaeten.Add(universitaet);
                        }
                        reader.Close();
                    }

                    // Abfrage für Personen
                    string personQuery = "SELECT PersonID, Vorname, Nachname, UniversitaetID FROM Personen";
                    using (SqlCommand command = new SqlCommand(personQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Person person = new Person
                            {
                                PersonID = reader.GetInt32(0),
                                Vorname = reader.GetString(1),
                                Nachname = reader.GetString(2),
                                UniversitaetID = reader.GetInt32(3),
                                Universitaet = universitaeten.FirstOrDefault(u => u.UniversitaetID == reader.GetInt32(3)) // Universitaet aus der Liste zuweisen
                            };
                            personen.Add(person);
                        }
                        reader.Close();
                    }

                    connection.Close();
                }

                // Manuell eine neue Universität hinzufügen
                Universitaet neueUniversitaet = new Universitaet
                {
                    UniversitaetID = 999, // Neue ID für die Universität
                    Name = "Neue Universität",
                    Standort = "Berlin"
                };
                universitaeten.Add(neueUniversitaet);

                // Manuell eine neue Person hinzufügen
                Person neuePerson = new Person
                {
                    PersonID = 9999, // Neue ID für die Person
                    Vorname = "Max",
                    Nachname = "Mustermann",
                    UniversitaetID = 999, // ID der Universität, zu der die Person gehört
                    Universitaet = neueUniversitaet // Setzen der Universität
                };
                personen.Add(neuePerson);

                // Überprüfen, ob Daten abgerufen wurden
                if (universitaeten.Count == 0)
                {
                    Console.WriteLine("Keine Universitäten gefunden.");
                }
                else
                {
                    Console.WriteLine($"Es wurden {universitaeten.Count} Universitäten gefunden.");
                }

                if (personen.Count == 0)
                {
                    Console.WriteLine("Keine Personen gefunden.");
                }
                else
                {
                    Console.WriteLine($"Es wurden {personen.Count} Personen gefunden.");
                }

                // LINQ-Abfrage: Personen, deren Vorname "Max" ist
                IEnumerable<Person> gefiltertePersonen = from person in personen
                                                         where person.Vorname == "Max"
                                                         select person;

                // Ergebnisse ausgeben
                Console.WriteLine("Gefilterte Personen (Vorname 'Max'):");

                foreach (Person person in gefiltertePersonen)
                {
                    Console.WriteLine($"{person.Vorname} {person.Nachname} - Universität: {person.Universitaet?.Name}");
                }

                // LINQ-Abfrage: Alle Universitäten und die Anzahl der Personen
                var universitaetMitPersonen = from uni in universitaeten
                                              join person in personen on uni.UniversitaetID equals person.UniversitaetID
                                              group person by uni.Name into uniGroup
                                              select new
                                              {
                                                  UniversitaetName = uniGroup.Key,
                                                  PersonenCount = uniGroup.Count()
                                              };

                // Ausgabe der geladenen Universitäten
                Console.WriteLine("Universitäten:");
                foreach (var universitaet in universitaeten)
                {
                    Console.WriteLine($"ID: {universitaet.UniversitaetID}, Name: {universitaet.Name}, Standort: {universitaet.Standort}");
                }

                // Ausgabe der geladenen Personen
                Console.WriteLine("\nPersonen:");
                foreach (var person in personen)
                {
                    Console.WriteLine($"ID: {person.PersonID}, Vorname: {person.Vorname}, Nachname: {person.Nachname}, UniversitätID: {person.UniversitaetID}");
                }

                // Ausgabe der Universitäten mit Personenzahl
                Console.WriteLine("\nUniversitäten mit Personenzahl:");
                foreach (var item in universitaetMitPersonen)
                {
                    Console.WriteLine($"Universität: {item.UniversitaetName}, Anzahl der Personen: {item.PersonenCount}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }

            Console.ReadKey(); // wartet auf einen Tastendruck, um das Programm zu schließen
        }
    }
}
