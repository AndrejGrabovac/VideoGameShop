using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp136
{
    class Program
    {
        public static string GetFile()
        {
            string fileName = "GameShop.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            return filePath;
        }
        
        public static void Povratak()
        {
            Console.WriteLine("Pritisnite ENTER ili bilo koju tipku za povratak u izbornik.");
            Console.ReadKey();
        }

        public struct Game
        {
            public string imeIgre, imeProizvodaca, kategorija;

            public int cijenaProdaje;
            public int cijenaPosudbe;
            public int status;

            public void IspisAll()
            {
                Console.WriteLine("Ime igre: {0}", this.imeIgre);
                Console.WriteLine("Proizvođac: {0}", this.imeProizvodaca);
                Console.WriteLine("Kategorija: {0}", this.kategorija);
                Console.WriteLine("Cijena prodaje: {0}", this.cijenaProdaje);
                Console.WriteLine("Cijena posudbe: {0}", this.cijenaPosudbe);
                Console.WriteLine(string.Empty);
            }       
            
            public string Line()
            {
                string line = this.imeIgre + "#" + this.imeProizvodaca + "#" + this.kategorija + "#" + this.cijenaProdaje + "#" + this.cijenaPosudbe + "#" + this.status;
                return line;
            }
        }

        static void Main(string[] args)
        {
            string[] naslov =
        {
            @"__      ___     _               _____                         _____ _                 
\ \    / (_)   | |             / ____|                       / ____| |                
 \ \  / / _  __| | ___  ___   | |  __  __ _ _ __ ___   ___  | (___ | |__   ___  _ __  
  \ \/ / | |/ _` |/ _ \/ _ \  | | |_ |/ _` | '_ ` _ \ / _ \  \___ \| '_ \ / _ \| '_ \ 
   \  /  | | (_| |  __/ (_) | | |__| | (_| | | | | | |  __/  ____) | | | | (_) | |_) |
    \/   |_|\__,_|\___|\___/   \_____|\__,_|_| |_| |_|\___| |_____/|_| |_|\___/| .__/ 
                                                                               | |    
                                                                               |_|  "

        };

            int intTemp;

            do
            {
                Console.Clear();

                Console.WriteLine(naslov[0]);
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("Odaberite jednu od mogucih kategorija:");
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("1.Unesi novu igru");
                Console.WriteLine("2.Video Igre i Proizvodaci");
                Console.WriteLine("3.Izbriši");
                Console.WriteLine("4.Uredi");
                Console.WriteLine("5.Financije");
                Console.WriteLine("6.Izađi");
                Console.WriteLine(new string('-', 40));

                DateTime curTime = DateTime.Now;

                Console.WriteLine(curTime);

                int.TryParse(Console.ReadLine(), out intTemp);

                Console.Clear();

                switch (intTemp)
                {
                    case 1:

                        Unesinovo();

                        break;

                    case 2:

                        StanjeUShopu();

                        break;

                    case 3:

                        DeleteRecord();

                        break;

                    case 4:

                        Editrecord();

                        break;

                    case 5:

                        Financije();

                        break;

                    case 6:

                        Console.WriteLine("Doviđenja!");

                        Console.ReadLine();

                        Environment.Exit(0); 

                        break;

                    default:

                        Console.WriteLine("Krivi unos!");

                        break;
                }
            } while (intTemp != 6);
        }

        static void Financije()
        {
            List<Game> nizGame = GetGamesFromFile();

            var ukupnaZarada = 0.0;

            foreach (var game in nizGame)
            {
                if (game.status == 0)
                {
                    ukupnaZarada += game.cijenaProdaje;
                }

                else if (game.status == 1)
                {
                    ukupnaZarada += game.cijenaPosudbe;
                }
            }
            Console.WriteLine("Ukupna zarada je: " + ukupnaZarada);

            Povratak();
        }

        static void Unesinovo()
        {
            ReadAll();

            Console.WriteLine("Unesite ime igre: ");
            string name = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Unesite proizvodaca: ");
            string proizvodac = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Unesite kategoriju igre: ");
            string kategorija = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Unesite 0 ako je igra prodana, 1, ako je posudena, 2 ako nije ništa od toga: ");
            int status = UnesiUspjesnoStatus();

            int cijenaProdaje = 0;
            int cijenaPosudbe = 0;
            if (status == 0)
            {
                Console.WriteLine("Unesite cijenu prodaje: ");
                 cijenaProdaje = UnesiUspjesno();
            } 

            else if (status == 1)
            {
                Console.WriteLine("Unesite cijenu posudbe: ");
                cijenaPosudbe = UnesiUspjesno();
            }

            string line = name + "#" + proizvodac + "#" + kategorija + "#" + cijenaProdaje + "#" + cijenaPosudbe + "#" + status;

            File.AppendAllText(GetFile(), line + Environment.NewLine);
        }

        static int UnesiUspjesno()
        {
            int polje;

            bool uspjesno;

            do
            {
                uspjesno = int.TryParse(Console.ReadLine(), out polje);

            }
            while (uspjesno == false);

            return polje;
        }

        static int UnesiUspjesnoStatus()
        {
            int polje;
            bool uspjesno;
            do
            {
                uspjesno = int.TryParse(Console.ReadLine(), out polje);

            } 
            while (uspjesno == false || polje < 0 || polje > 3);

            return polje;
        }      

        static List<Game> GetGamesFromFile()
        {
            List<Game> nizGame = new List<Game>();

            using (StreamReader sr = File.OpenText(GetFile()))
            { 
                string line = "";

                while ((line = sr.ReadLine()) != null)
                { 
                    String[] sadrzaj = line.Split('#');

                    Game novigame = new Game();

                    novigame.imeIgre = sadrzaj[0];

                    novigame.imeProizvodaca = sadrzaj[1];

                    novigame.kategorija = sadrzaj[2];

                    novigame.cijenaProdaje = int.Parse(sadrzaj[3]);

                    novigame.cijenaPosudbe = int.Parse(sadrzaj[4]);

                    novigame.status = int.Parse(sadrzaj[5]);

                    nizGame.Add(novigame);
                }
            }

            return nizGame;
        }

        static void StanjeUShopu()
        {
            List<Game> nizGame = GetGamesFromFile();

            var prodane = nizGame.Where(x => x.status == 0);
            var posudene = nizGame.Where(x => x.status == 1);
            var naStanju = nizGame.Where(x => x.status == 2);

            Console.WriteLine("PRODANE IGRE: " + Environment.NewLine);

            foreach (var item in prodane)
            {
                item.IspisAll();
            }

            Console.WriteLine("POSUĐENE IGRE: " + Environment.NewLine);

            foreach (var item in posudene)
            {
                item.IspisAll();
            }

            Console.WriteLine("IGRE NA STANJU: " + Environment.NewLine);

            foreach (var item in naStanju)
            {
                item.IspisAll();
            }

            Povratak();
        }

        static List<Game> ReadAll()
        {
            List<Game> nizGG = GetGamesFromFile();

            PrintAll(nizGG);
            return nizGG;
        }

        static void PrintAll(List<Game> nizGG)
        {
            foreach (Game i in nizGG)
            {
                Console.WriteLine("");

                i.IspisAll();

                Console.WriteLine("");
            }
        }

        static void DeleteRecord()
        {
            
            var nizIgra = ReadAll();
            Console.WriteLine(new string ('-', 40));
            
            Console.WriteLine("Unesite ime igre koju zelite izbrisati: ");

            var ime = Console.ReadLine();

            var LineNumberToDelete = -1;

            var i = 0;

            foreach (Game gameshop in nizIgra)
            {
                if (gameshop.imeIgre == ime)
                {
                    LineNumberToDelete = i;
                }
                i++;
            }

            if (LineNumberToDelete == -1)
            {
                Console.WriteLine(ime + " ne postoji na stanju!");

                Console.WriteLine(string.Empty);

                Console.WriteLine("Pritisnite ENTER ili bilo koju tipku za povratak u izbornik.");

                Console.ReadKey();

                return;
            }
            nizIgra.RemoveAt(LineNumberToDelete);

            String lineForWrite = "";

            foreach (Game gameshop in nizIgra)
            {
                lineForWrite += gameshop.Line() + Environment.NewLine;
            }
            File.WriteAllText(GetFile(), lineForWrite);
        }

        static void Editrecord()
        {
            ReadAll();

            Console.WriteLine("Unesite ime igre koju želite urediti: ");

            var name = Console.ReadLine();

            string sourceFile = GetFile();

            string currentLineText = null;

            var linenumber = -1;
            var i = 1;
            var oldLine = "";

            using (StreamReader reader = new StreamReader(sourceFile))
            {
                while ((currentLineText = reader.ReadLine()) != null)
                {
                    var nameFromFile = currentLineText.Split('#')[0];

                    if (nameFromFile == name)
                    {
                        linenumber = i;
                        oldLine = currentLineText;
                    }
                    i++;
                }
            }

            if (linenumber == -1)
            {
                Console.WriteLine(name + " ne postoji u arhivi!");

                Console.WriteLine(string.Empty);

                Povratak();

                return;
            }

            Console.WriteLine();

            Console.WriteLine("Unesite novo ime: ");
            var newIme = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Unesite novog proizvođaca: ");
            var newProducer = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Unesite novu kategoriju: ");
            var newKategorija = Console.ReadLine();

            Console.WriteLine();

            var oldLineSplitted = oldLine.Split('#');
            var newlineToWrite = newIme + "#" + newProducer + "#" + newKategorija + "#" + oldLineSplitted[3] + "#" + oldLineSplitted[4] + "#" + oldLineSplitted[5];

            string[] lines = File.ReadAllLines(sourceFile);

            using (StreamWriter writer = new StreamWriter(sourceFile))
            {
                for (i = 1; i <= lines.Length; i++)
                {
                    if (i == linenumber)
                    {
                        writer.WriteLine(newlineToWrite);
                    }

                    else
                    {
                        writer.WriteLine(lines[i - 1]);
                    }
                }
            }

        }

    }
}
