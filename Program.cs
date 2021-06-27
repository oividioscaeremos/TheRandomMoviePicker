using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Globalization;

namespace FilmTayfa_Random
{
    class Program
    {
        private List<Tuple<int, int>> winnerWinnerChickenDinner;
        public Program()
        {
            winnerWinnerChickenDinner = new List<Tuple<int, int>>();
        }

        private void GetResults(Dictionary<string, string> personMoviePairs)
        {
            int round = 1;

            personMoviePairs = RandomlyDistributedPairs(personMoviePairs);

            while (winnerWinnerChickenDinner.Where(tuple => tuple.Item2 == 3).FirstOrDefault() == null)
            {
                var returnValue = GetRandomlyPickedIndice(personMoviePairs, true);
                if(winnerWinnerChickenDinner.Where(val => val.Item1 == returnValue).FirstOrDefault() != null)
                {
                    winnerWinnerChickenDinner = winnerWinnerChickenDinner.Select(tupleVal =>
                    {
                        if(tupleVal.Item1 == returnValue)
                        {
                            return Tuple.Create(tupleVal.Item1, tupleVal.Item2 + 1);
                        }
                        return Tuple.Create(tupleVal.Item1, tupleVal.Item2);
                    }).ToList();
                }
                else
                {
                    winnerWinnerChickenDinner.Add(new Tuple<int, int>(item1: returnValue, 1));
                }
                Console.WriteLine();
                Console.WriteLine($"    Round {round++} - Kazanan : {personMoviePairs.ToList()[returnValue].Key.ToUpper(CultureInfo.GetCultureInfo("tr-TR"))} - {new CultureInfo("en-US").TextInfo.ToTitleCase(personMoviePairs.ToList()[returnValue].Value)}    [   {winnerWinnerChickenDinner.Where(tuple => tuple.Item1 == returnValue).First().Item2}. Sefer   ]");
                Console.WriteLine();
            }
            var kazananTuple = winnerWinnerChickenDinner.Where(tuple => tuple.Item2 == 3).First();
            Console.WriteLine("");
            Console.WriteLine($"    ~~~~ GECENIN KAZANANI ~~~ {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}");
            Console.WriteLine("");
            Console.WriteLine($"         {personMoviePairs.ToList()[kazananTuple.Item1].Key.ToUpper(CultureInfo.GetCultureInfo("tr-TR"))} - {new CultureInfo("en-US").TextInfo.ToTitleCase(personMoviePairs.ToList()[kazananTuple.Item1].Value)}");
            Console.WriteLine("");
            personMoviePairs.ToList().ForEach(personMoviePair => 
            {
                if(winnerWinnerChickenDinner.Find(contestant => contestant.Item1 == personMoviePairs.ToList().IndexOf(personMoviePair)) != null)
                {
                    Console.WriteLine($"    {personMoviePairs.ToList().IndexOf(personMoviePair) + 1}: {personMoviePair.Key.ToUpper(CultureInfo.GetCultureInfo("tr-TR"))} - {new CultureInfo("en-US").TextInfo.ToTitleCase(personMoviePair.Value)} - {winnerWinnerChickenDinner.Find(s => s.Item1 == personMoviePairs.ToList().IndexOf(personMoviePair)).Item2}");
                }
                else
                {
                    Console.WriteLine($"    {personMoviePairs.ToList().IndexOf(personMoviePair) + 1}: {personMoviePair.Key.ToUpper(CultureInfo.GetCultureInfo("tr-TR"))} - {new CultureInfo("en-US").TextInfo.ToTitleCase(personMoviePair.Value)} - {0}");
                }
            });
            Console.WriteLine("");
            Console.WriteLine("");


        }

        private int GetRandomlyPickedIndice(Dictionary<string, string> personMoviePairs, bool shouldWait)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[personMoviePairs.Count];
            rng.GetBytes(buffer);
            int res = BitConverter.ToInt32(buffer, 0);
            var rndom = new Random(res).Next(0, personMoviePairs.Count);
            if (shouldWait)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("    3...");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("    2...");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("    1...");
            }
            
            return rndom;
        }

        private Dictionary<string, string> RandomlyDistributedPairs(Dictionary<string, string> keyValuePairs)
        {
            int percentile = (100 / keyValuePairs.Count);
            int order = 1;
            var secondKeyValuePairs = new Dictionary<string, string>();
            var newArr = Enumerable.Repeat(-1, keyValuePairs.Count).ToArray();
            Console.WriteLine("    Liste yeniden düzenleniyor..");
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);

            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                int from = percentile * i;
                int to = percentile * (i + 1);
                for(int j = from; j < to; j++)
                {
                    Console.Write($"\r    %{j}");
                    System.Threading.Thread.Sleep(25);
                }
                if(i == keyValuePairs.Count - 1 && to != 100)
                {
                    for (int j = to; j <= 100; j++)
                    {
                        Console.Write($"\r    %{j}");
                        System.Threading.Thread.Sleep(25);
                    }
                }
                int randomIndice = GetRandomlyPickedIndice(keyValuePairs, false);

                while (newArr.ToList().Contains(randomIndice))
                {
                    randomIndice = GetRandomlyPickedIndice(keyValuePairs, false);
                }
                newArr[i] = randomIndice;
            }
            Console.WriteLine("   Liste düzenlenmesi bitti.");
            Console.WriteLine();
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                secondKeyValuePairs.Add(keyValuePairs.ToList()[newArr[i]].Key, keyValuePairs.ToList()[newArr[i]].Value);
            }

            foreach (var pair in secondKeyValuePairs)
            {
                Console.WriteLine($"    {order++}: {pair.Key.ToUpper(CultureInfo.GetCultureInfo("tr-TR"))} - {new CultureInfo("en-US").TextInfo.ToTitleCase(pair.Value)}");
            }
            Console.WriteLine();

            return secondKeyValuePairs;
        }


        static void Main(string[] args)
        {
            var program = new Program();
            //WindowUtility.MoveWindowToTopLeft();
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WriteLine();
            Console.WriteLine("    Hazırsak Başlıyoruz!");
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);

            program.GetResults(new Dictionary<string, string> {
                {  "ilker atabay", "rememory" },
                /*{  "ulas soyubey", "movie2" },*/
                {  "berkay burak", "polar" },
                /*{  "rümeysa", "movie4" },*/
                /*{  "ahmet burak", "movie5" },*/
                {  "şafak", "kara büyü" },
                /*{  "bünyamin", "movie7" },*/
                {  "kırmızı", "rudderless" },
                /*{  "emin", "sin city" },*/
            });

            Console.ReadKey();
        }
    }
}
