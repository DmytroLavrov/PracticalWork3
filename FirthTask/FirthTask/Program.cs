using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace FirthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] ways =
            {
                @"/Users/dmytrolavrov/Projects/PracticalWork3/FirthTask/FirthTask/TextBlockchain1.txt",
                @"/Users/dmytrolavrov/Projects/PracticalWork3/FirthTask/FirthTask/TextBlockchain2.txt",
                @"/Users/dmytrolavrov/Projects/PracticalWork3/FirthTask/FirthTask/TextBlockchain3.txt",
            };

            List<string> texts = new List<string>();

            foreach (string way in ways)
            {
                using (StreamReader streamReader = new StreamReader(way))
                {
                    texts.Add(streamReader.ReadToEnd());
                }
            }

            Func<string, IEnumerable<string>> tokenize = Tokenize;
            Func<IEnumerable<string>, IDictionary<string, int>> countOfWords = Count;
            Action<IDictionary<string, int>> statistic = Statistic;
            IEnumerable<string> terms = texts.SelectMany(tokenize);
            IDictionary<string, int> periodicity = countOfWords(terms);
            Statistic(periodicity);
        }

        static IDictionary<string, int> Count(IEnumerable<string> terms)
        {
            Dictionary<string, int> periodicity = new Dictionary<string, int>();
            foreach (string term in terms)
            {
                if (periodicity.ContainsKey(term))
                {
                    periodicity[term]++;
                }
                else
                {
                    periodicity[term] = 1;
                }
            }
            return periodicity;
        }

        static IEnumerable<string> Tokenize(string text)
        {
            char[] separators = { ' ', '\t', '.', ',', '\r', ';', ':', '\n', '!', '\"', '\'', '?' };
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        static void Statistic(IDictionary<string, int> periodicity)
        {
            foreach (var countOfWord in periodicity)
            {
                Console.WriteLine($"{countOfWord.Key} - {countOfWord.Value}");
            }
            Console.ReadKey();
        }
    }
}