using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Taxi
{
    internal class Program

    {
        static List<Fuvar> fuvarok;
        static void Main(string[] args)
        {
            // Ellenőrzés:

            /*       var sorok = File.ReadAllLines("fuvar (1).csv");

                     foreach (var sor in sorok)
                     {
                         Console.WriteLine(sor);

                     }
             */


            // 3.Feladat: Határozza meg és írja ki a képernyőre a minta szerint, hogy hany utazás került
            //             feljegyzésre az áIlományban.



            //     Console.WriteLine($" 3.feladat: {fuvarok.Count()} fuvar");



            List<Fuvar> fuvarok = new List<Fuvar>();


            string[] sorok = File.ReadAllLines("fuvar (1).csv");
            for (int i = 1; i < sorok.Length; i++)
            {
                string[] data = sorok[i].Split(';');

                int taxiAzonosito = int.Parse(data[0]);
                string indulasIdopontja = data[1];
                int utazasIdotartam = int.Parse(data[2]);
                double megtettTavolsag = double.Parse(data[3]);
                double viteldij = double.Parse(data[4]);
                double borravalo = double.Parse(data[5]);
                string fizetesModja = data[6];


                Fuvar fuvar = new Fuvar(taxiAzonosito, indulasIdopontja, utazasIdotartam, megtettTavolsag, viteldij, borravalo, fizetesModja);
                fuvarok.Add(fuvar);
            }

            Console.WriteLine($"3. feladat:  {fuvarok.Count()} fuvarok");

            //--------------------------------------------------------------------------

            // 4.feladat:     Határozza meg és írja ki a képernyőre a minta szerint, hogy a 6185 - ös azonosítójú
            //               taxisnak mennyi volt a bevétele, és ez hány fuvarból állt!
            //               FeltéteIezheti, hogy van ilyen azonosítójú taxis.


            double bevetel = fuvarok.Where(sor => sor.TaxiAzonosito == 6185)
                                    .Sum(sor => sor.Viteldij + sor.Borravalo);

            int fuvarokSzama = fuvarok.Where(sor => sor.TaxiAzonosito == 6185)
                                      .Count();

            Console.WriteLine($"4. feladat:  {fuvarokSzama} fuvar  alatt:  {bevetel}$");

            //----------------------------------------------------------------------------

            /* 5. feladat:   Programjával határozza meg az áIlomány adataibő| a fizetési módokat, majd összesítse'
                             hogy aZ egyes fizetési módokat hányszor választották az utak során!
                             Ezeket aZ eredményeket a minta szerint írja a képernyőre!
                             A kiírás során a fizetési módok sorrendje bármilyen lehet.   */

            Console.WriteLine("5.feladat:");
            var fizetesekModja = fuvarok.Select(sor => sor.FizetesModja)
                                        .Distinct();


            foreach (var fizetesModja in fizetesekModja)
            {
                int adatok = fuvarok
                             .Count(sor => sor.FizetesModja == fizetesModja);

                Console.WriteLine($" \t{fizetesModja}: {adatok} fuvar");
            }


            // Másik megoldás: 

            Console.WriteLine("5.feladat:");

            // Fizetési módok összesítése és tárolása egy szótárban
            Dictionary<string, int> fizetesiModokSzama = new Dictionary<string, int>();

            foreach (Fuvar fuvar in fuvarok)
            {
                string fizetesModja = fuvar.FizetesModja;

                if (fizetesiModokSzama.ContainsKey(fizetesModja))
                {
                    fizetesiModokSzama[fizetesModja]++;
                }
                else
                {
                    fizetesiModokSzama[fizetesModja] = 1;
                }
            }


            foreach (var kvp in fizetesiModokSzama)
            {
                Console.WriteLine($"\t{kvp.Key}: {kvp.Value} fuvar");
            }


            //----------------------------------------------------------------------

            /* 6.feladat: Határozza meg és írja ki a képernyőre a minta szerint, hogy osszesen hány km-t tettek
                          meg a taxisok(1 mérfold: 1, 6 km)! Az eredményt két tizedesjegyre kerekítve jelenítse
                          meg a képernyőn!   */

            Console.WriteLine($"6.feladat: {Math.Round(fuvarok.Sum(sor => sor.MegtettTavolsag * 1.6), 2)}km");

            //-------------------------------------------------------------------------

            /* 7.feladat: Hatátozza meg és írja ki a képemyőre a minta szerint az időben leghosszabb fuvar adataitt'
                          Feltételezheti, hogy nem alakult ki holtverseny.      */


            /*  var ido = fuvarok.Max(sor => sor.UtazasIdotartam);
                 Console.WriteLine($"\tFuvar hossza: {ido} másodperc");  */

            Fuvar leghosszabbFuvar = fuvarok.OrderByDescending(f => f.UtazasIdotartam).FirstOrDefault();

            if (leghosszabbFuvar != null)
            {

                Console.WriteLine("7.feladat: Leghosszabb fuvar:");
                Console.WriteLine($"\tFuvar hossza: {leghosszabbFuvar.UtazasIdotartam} másodperc");
                Console.WriteLine($"\tTaxi azonosítója: {leghosszabbFuvar.TaxiAzonosito}");
                Console.WriteLine($"\tMegtett távolság: {leghosszabbFuvar.MegtettTavolsag} km");
                Console.WriteLine($"\tViteldíj: {leghosszabbFuvar.Viteldij}$");

            }

            //---------------------------------------------------------------------

            /* 8.feladat: Hozzon létre hibak. txt néven egy UTF.8 kódolású szöveges állományt, ami
                          tartalmazza azokat az adatokat, amelyek esetében hiba van az ercdeti állományban! 
                          Hibás sornak tekintjük azokat az eseteket, amelyekben aZ utazás időtartama és a viteldíj 
                          egy nullánál nagyobb érték, de a hozzátartoző megtett távolság értéke nulla.
                          A sorok indulási időpont szerint növekvő rendben legyenek az áIlományban!
                          A hibak.txt állomány szerkezete egyezzen meg a fuvar.csv állomány szerkezetével!  */

            List<Fuvar> hibasFuvarok = fuvarok
            .Where(f => f.UtazasIdotartam > 0 && f.Viteldij > 0 && f.MegtettTavolsag == 0)
            .OrderBy(f => DateTime.Parse(f.IndulasIdopontja))
            .ToList();

            
            SaveDataToFile("hibak.txt", hibasFuvarok);

            using (StreamWriter writer = new StreamWriter("hibak.txt", false, Encoding.UTF8))
            {
                
                writer.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");

                foreach (Fuvar fuvar in fuvarok)
                {
                    
                    writer.WriteLine($"{fuvar.TaxiAzonosito};" +
                                     $"{fuvar.IndulasIdopontja};" +
                                     $"{fuvar.UtazasIdotartam};" +
                                     $"{fuvar.MegtettTavolsag};" +
                                     $"{fuvar.Viteldij};" +
                                     $"{fuvar.Borravalo};" +
                                     $"{fuvar.FizetesModja}");
                }
            }

            Console.WriteLine($"8.feladat: {"hibak.txt"}");

}

        private static void SaveDataToFile(string v, List<Fuvar> hibasFuvarok)
            {
                
            }
        }
  
 }
    
