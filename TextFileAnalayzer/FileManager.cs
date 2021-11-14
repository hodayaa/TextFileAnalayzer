using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TextFileAnalayzer
{
    public static class FileManage
    {
        //#1 Calculate the number of lines in the file
        public static int CountLines(FileInfo file)
        {
            return File.ReadLines(file.FullName).Count();
        }

        public static List<string> Words(FileInfo file)
        {
            List<string> wordsTxt;
            string text = File.ReadAllText(file.FullName);
            var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
            wordsTxt = text.ToLower().Split().Select(x => x.Trim(punctuation)).ToList();
            wordsTxt.RemoveAll(s => s == "");
            return wordsTxt;
        }

        public static List<string> Words(string sentence)
        {
            List<string> wordsTxt;
            string text = sentence;
            var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
            wordsTxt = text.ToLower().Split().Select(x => x.Trim(punctuation)).ToList();
            wordsTxt.RemoveAll(s => s == "");
            return wordsTxt;
        }

        //#2 Calculate the number of words in the file
        public static long CountWords(FileInfo file)
        {
            List<string> wordsTxt = Words(file);//File.ReadAllText(file.FullName).Split(new Char[] { ' ', '\n', '\r' }).ToList();

            return wordsTxt.Count();
        }


        //Calculate the number of words in the sentence
        public static long CountWords(string sentence)
        {
            List<string> wordsTxt = Words(sentence);//sentence.Split(new Char[] { ' ', '\n', '\r' }).ToList();

            return wordsTxt.Count();
        }

        //לבדוק אם צריך להתיחס למילים מחוברות כמו I'm...
        //#3 Calculate the number of unique words in the file
        public static int CountUniqueWords(FileInfo file)
        {
            List<string> wordsTxt = Words(file); //File.ReadAllText(file.FullName).ToLower().Split(new Char[] { ' ', '\n', '\r' }).ToList();

            IEnumerable<string> uniqueWords = wordsTxt.GroupBy(w => w).Where(g => g.Count() == 1).Select(g => g.Key).ToList();

            //int counterUnique = 0;
            //for (int i = 0; i < wordsTxt.Count(); i++)
            //{
            //    for (int j = i+1; j < wordsTxt.Count(); j++)
            //    {
            //        if (wordsTxt[j].Equals(wordsTxt[i]))
            //        {
            //            wordsTxt.RemoveAll(s => s.Equals(wordsTxt[i]));
            //            i--;
            //            break;
            //        }
            //    }                   
            //}
            //foreach (string word in wordsTxt)
            //{
            //    if (wordsTxt.Count(s => s == word) == 1)
            //    {
            //        counterUnique++;
            //    }
            //}
            //return counterUnique;

            return uniqueWords.Count();

        }


        //#4 Calculate the Max and Avg of words in the sentence in the file
        public static int[] MaxAvgSentence(FileInfo file)
        {
            List<string> sentenceTxt = File.ReadAllText(file.FullName).Split('.').ToList(); // new Char[] { '.', '\n', '?' , '!'}).ToList();
            sentenceTxt.RemoveAll(s => s == ""); //if the text has three points etc.
            int avgSentence = 0;
            int maxSentence = 0;
            int numWords = 0;

            foreach (var sentence in sentenceTxt)
            {
                numWords = (int)CountWords(sentence);
                avgSentence += numWords;
                maxSentence = maxSentence < numWords ? numWords : maxSentence;
            }

            return new int[2] { maxSentence, avgSentence / sentenceTxt.Count() }; //(int)(CountWords(file) / sentenceTxt.Count()) };
        }


        //לבדוק אם צריך להתיחס למילים מחוברות עם גרש כמו I'm...
        //#5 Find the popular word in the text
        public static string FindPopularWord(FileInfo file)
        {
            List<string> wordsTxt = Words(file); //File.ReadAllText(file.FullName).Split(new Char[] { ' ', '\n', '\r' }).ToList();

            string popularWord = "";
            int counterPopular = 1;
            foreach (string word in wordsTxt)
            {
                int count = wordsTxt.Count(s => s == word);
                if (count > counterPopular)
                {
                    counterPopular = count;
                    popularWord = word;
                }
            }
            return popularWord;

        }

        //#5 Find the popular word in the text without syntax words- not finished!!!
        public static string FindPopularWordWithoutSyntax(FileInfo file)
        {
            List<string> wordsTxt = Words(file); //File.ReadAllText(file.FullName).Split(new Char[] { ' ', '\n', '\r' }).ToList();


            string popularWord = "";
            int counterPopular = 1;
            foreach (string word in wordsTxt)
            {
                int count = wordsTxt.Count(s => s == word);
                if (count > counterPopular)
                {
                    counterPopular = count;
                    popularWord = word;
                }
            }
            return popularWord;

        }

        //#6 Find the Max  words in the text without K
        public static string MaxWordsWithoutK(FileInfo file)
        {
            string maxWords = "";
            int max = 0;
            List<string> wordsTxt = Words(file); //File.ReadAllText(file.FullName).Split(new Char[] { ' ', '\n', '\r' }).ToList();
            //wordsTxt.RemoveAll(s => s == "");
            for (int i = 0; i < wordsTxt.Count(); i++)
            {
                string words = "";
                int countWords = 0;
                while (i < wordsTxt.Count() && !wordsTxt[i].Contains('k') && !wordsTxt[i].Contains('K'))
                {
                    countWords++;
                    words += wordsTxt[i] + " ";
                    i++;
                }
                if (countWords > max)
                {
                    max = countWords;
                    maxWords = words;
                }
            }
            return maxWords;
        }

        //#8 Find the colors in the text and calculate the times
        public static Dictionary<string, int> CountColors(FileInfo file)
        {
            List<string> colors = new List<string>();
            Dictionary<string, int> colorsInTxt = new Dictionary<string, int>();
            KnownColor[] kcolors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor knowColor in kcolors)
            {
                Color color = Color.FromKnownColor(knowColor);
                colors.Add(color.Name.ToLower());
            }
            List<string> wordsTxt = Words(file); //File.ReadAllText(file.FullName).Split(new Char[] { ' ', '\n', '\r' }).ToList();
            //wordsTxt.RemoveAll(s => s == "");
            foreach (string word in wordsTxt)
            {
                if (colors.Contains(word.ToLower()))
                {
                    if (colorsInTxt.Keys.Contains(word.ToLower()))
                    {
                        colorsInTxt[word.ToLower()]++;
                    }
                    else
                    {
                        colorsInTxt.Add(word.ToLower(), 1);
                    }
                }
            }

            return colorsInTxt;
        }
    }
}