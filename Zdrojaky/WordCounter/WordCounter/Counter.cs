using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCounter
{
    /// <summary>
    /// Trieda na reprezentaciu poctu slov v suboer
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// konstruktor triedy
        /// </summary>
        /// <param name="fileContent">obsah suboru</param>
        public Counter(string fileContent)
        {
            // interpunkcia
            var punctuation = fileContent.Where(Char.IsPunctuation).Distinct().ToArray();
            // rozdelim obsah suboru a orezem interpunkciu
            var words = fileContent.ToLower().Split().Select(x => x.Trim(punctuation));
            // vyberiem unikatne rozparsovane stringy dlhsie ako 0 znakov
            var uniqueWords = words.Where(x => x.Length > 0).Distinct().ToArray();
            // naplnim dictionary
            uniqueWords.ToList().ForEach(x => wordsAsIs.Add(x, words.Where(s => s == x).Count()));

        }

        /// <summary>
        /// pocty slov v poradi v akom boli nacitane zo suboru
        /// </summary>
        private Dictionary<string, int> wordsAsIs = new Dictionary<string, int>();
        /// <summary>
        /// pocty slov zoradene podla abecedy
        /// </summary>
        private Dictionary<string, int> OrderedAlphabet => wordsAsIs.OrderBy(x => x.Key).ToDictionary(item => item.Key, item => item.Value);
        /// <summary>
        /// pocty slov zoradene podla vyskytu
        /// </summary>
        private Dictionary<string, int> OrderedByCount => wordsAsIs.OrderBy(x => x.Key).OrderByDescending(x => x.Value).ToDictionary(item => item.Key, item => item.Value);

        /// <summary>
        /// prepisanie metody to string - umozni ziskat vystup v dobe vystupneho retazca
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string outputString =

            $"*** Slova v poradi v sobore ***\n{GenerateOutputString(wordsAsIs)}" +
            $"\n\n*** Slova podla abacedy ***\n{GenerateOutputString(OrderedAlphabet)}" +
            $"\n\n*** Slova podla vyskytu ***\n{GenerateOutputString(OrderedByCount)}";

            return outputString;
        }

        /// <summary>
        /// metoda na generovanie retazca hodnot z dictionary
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private string GenerateOutputString(Dictionary<string, int> dict)
        {
            string outputString = string.Empty;
            dict.ToList().ForEach
                (
                x =>
                {
                    outputString += $"\nSlovo: {x.Key}, pocet {x.Value}";
                }
                );
            return outputString;
        }
    }
}
