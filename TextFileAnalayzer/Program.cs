using System;
using System.Collections.Generic;
using System.IO;

namespace TextFileAnalayzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome For TFM- Text File Mananger!");
            Console.WriteLine("Enter path of file:");
            string path = Console.ReadLine();
            FileInfo file = new FileInfo(@path);

            int LinesCount = FileManage.CountLines(file);
            long WordsCount = FileManage.CountWords(file);
            int UniqWordsCount = FileManage.CountUniqueWords(file);
            int[] maxAvgSentence = FileManage.MaxAvgSentence(file);
            string wordsWithoutK = FileManage.MaxWordsWithoutK(file);
            Dictionary<string, int> colors = FileManage.CountColors(file);

            Console.WriteLine("Number of lines: " + LinesCount);
            Console.WriteLine("Number of words: " + WordsCount);
            Console.WriteLine("Number of unique words: " + UniqWordsCount);
            Console.WriteLine("Average sentence length: " + maxAvgSentence[1]);
            Console.WriteLine("Maximum sentence length: " + maxAvgSentence[0]);
            Console.WriteLine("The maximum words without K: " + wordsWithoutK);
            Console.WriteLine("The colors: ");
            foreach (KeyValuePair<string, int> color in colors)
            {
                Console.WriteLine(color.Key + ": " + color.Value);
            }
        }
    }
    
}
