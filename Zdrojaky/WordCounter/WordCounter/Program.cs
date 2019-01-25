using System;
using System.IO;
using System.Linq;

namespace WordCounter
{
    public class Program
    {
        static void Main(string[] args)
        {
            // ziaden parameter
            if (args.Count() == 0)
            {
                Console.WriteLine("Chybajuca cesta k soboru. Program bude ukonceny.");
                Console.ReadKey();
                return;
            }
            if (args.Count()>1)
            {
                Console.WriteLine("Zadanych privela vstupnych parametrov. Pocitat slova sa budu len pre prvy parameter.");
            }

            string filePath = args[0];
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch
            {
                Console.WriteLine("Chyba pri nacitani suboru. Program bude ukonceny");
                Console.ReadKey();
                return;
            }

            if (content == string.Empty)
            {
                Console.WriteLine("Prazdny subor. Program bude ukonceny");
                Console.Read();
                return;
            }

            Counter counter = new Counter(content);
            if (counter != null)
            {
                Console.WriteLine("Subor nacitany.");
                string outputFilePath = string.Empty;
                var result = WriteOutput(counter, filePath, out outputFilePath);
                if (result)
                    Console.WriteLine($"Vysledky su v subore {outputFilePath}.");
                else
                    Console.WriteLine("Chyba pri zapise vysledkov.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// metoda pre zapis vysledkov do suboru
        /// </summary>
        /// <param name="counter">pocitac slov</param>
        /// <param name="inputFilePath">cesta k vstupnemu suboru</param>
        /// <param name="outputFilePath">cestka k vystupnemu suboru</param>
        /// <returns>priznak, ci bol zapis do suboru uspesny</returns>
        private static bool WriteOutput(Counter counter, string inputFilePath, out string outputFilePath)
        {
            var directory = Path.GetDirectoryName(inputFilePath);
            var fileName = Path.GetFileNameWithoutExtension(inputFilePath);
            outputFilePath = $"{directory}\\{fileName}_output.txt";
            if (File.Exists(outputFilePath))
                Console.WriteLine("Existujuci subor s vysledkami bude prepisany.");
            string outputString = counter.ToString();
            try
            {
                File.WriteAllText(outputFilePath, outputString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
