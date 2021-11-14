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
            //#1
            int LinesCount = FileManage.CountLines(file);
            //#2
            long WordsCount = FileManage.CountWords(file);
            //#3
            int UniqWordsCount = FileManage.CountUniqueWords(file);
            //#4
            int[] maxAvgSentence = FileManage.MaxAvgSentence(file);
            //#6
            string wordsWithoutK = FileManage.MaxWordsWithoutK(file);
            //#8
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
