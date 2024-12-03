/*
statt var benutzt man normalerweise IEnumerable<datentyp>

IEnumerable<int> gefilterteZahlen = from zahl in zahlen
                                    where zahl > 5
                                    select zahl;
-------------------------------------------------------------------------------------------------------------------------------------------------------------

mit var wird der Typ automatisch bestimmt und die syntax ist kürzer!

var gefilterteZahlen = from zahl in zahlen
                                   where zahl > 5
                                   select zahl;

-------------------------------------------------------------------------------------------------------------------------------------------------------------

Erklärung
IEnumerable<int>

Das ist der spezifische Typ, der die gefilterten Zahlen repräsentiert.
IEnumerable steht für eine Sammlung, die sequenziell durchlaufen werden kann (z. B. in einer foreach-Schleife).
Warum wird oft var verwendet?

var ist praktischer, weil der Typ automatisch aus dem Kontext abgeleitet wird. 
Es macht den Code kürzer und lesbarer, insbesondere bei komplexeren LINQ-Abfragen.
Aber wenn du explizit angeben möchtest, was der Rückgabewert ist (z. B. zur besseren Lesbarkeit oder Typensicherheit), dann verwendest du IEnumerable<int>.

-------------------------------------------------------------------------------------------------------------------------------------------------------------
foreach (var item in collection)
            {

            }


-------------------------------------------------------------------------------------------------------------------------------------------------------------
Unterschied:
Console.ReadLine(): Wartet auf die Eingabe einer vollständigen Zeile (Drücken der Enter-Taste).
Console.ReadKey(): Wartet auf einen einzigen Tastendruck (keine Enter-Taste nötig).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ein Array von Zahlen
            int[] zahlen = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // LINQ-Abfragen: Collection == die gennate 'gefilterteZahlen' da soll eine zahl aus dem array 'zahlen' ausgegeben werden die größer > 5 ist!
            var gefilterteZahlen = from zahl in zahlen
                                   where zahl > 5
                                   select zahl;

            // Ergebnisse ausgeben
            Console.WriteLine("Zahlen größer als 5:");
            foreach (var zahl in gefilterteZahlen)
            {
                Console.WriteLine(zahl);
                
            }

            Console.ReadKey(); // wartet ausßerhalb der schleife auf einen tastendruck um das programm zu schließen
        }
    }
}
