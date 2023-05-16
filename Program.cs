using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab_3
{
    class Lab_3
    {
        static void Processing(string finName, string foutName)
        {
            int[] permutation = GetPermutation(finName);
            StringBuilder text = new StringBuilder();

            using (StreamReader streamReader = new StreamReader(finName))
            {
                SkipLines(streamReader, 5);
                text.Append(streamReader.ReadToEnd());
            }

            text.Replace("\r", String.Empty);
            text.Remove(text.Length - 1, 1);

            StringBuilder unscrambledText = new StringBuilder();

            while (text.Length > 0)
            {
                if (text.Length >= permutation.Length)
                {
                    StringBuilder textPart = new StringBuilder();
                    textPart.Append(text.ToString(0, permutation.Length));
                    text.Remove(0, permutation.Length);
                    unscrambledText.Append(Unscramble(textPart.ToString(), permutation));
                }
                else
                {
                    StringBuilder pieceOfText = new StringBuilder();
                    pieceOfText.Append(text.ToString(0, text.Length));
                    text.Remove(0, text.Length);
                    unscrambledText.Append(Unscramble(pieceOfText.ToString(), permutation));
                }
            }
            Print(foutName, unscrambledText, permutation);
        }

        static string Unscramble(string line, int[] permutations)
        {
            char[] chars = new char[line.Length];
            if (line.Length == permutations.Length)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    chars[i] = line[permutations[i]];
                }
            }
            else
            {
                for (int i = 0; i < line.Length; i++)
                {
                    int j = i;
                    do
                    {
                        j = permutations[j];
                    }
                    while (j >= line.Length);
                    chars[i] = line[j];
                }
            }
            return string.Join("", chars);
        }

        static int[] GetPermutation(string fileName)
        {
            string permutationLine;
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                SkipLines(streamReader, 1);
                permutationLine = streamReader.ReadLine();
            }
            permutationLine = permutationLine.Trim();

            string[] indexes = permutationLine.Split();
            List<int> permutation = new List<int>();

            foreach (string i in indexes)
            {
                if (i.Length != 0)
                    permutation.Add(int.Parse(i));
            }

            return permutation.ToArray();
        }

        static void Print(string foutName, StringBuilder unscrambledText, int[] permutation)
        {
            StreamWriter streamWriter = new StreamWriter(foutName);
            streamWriter.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            
            streamWriter.WriteLine($"Decripting {unscrambledText.Length} characters"); 

            streamWriter.Write($"Using:\t");
            for (int i = 0; i < permutation.Length; i++)
            {
                streamWriter.Write(i + "\t");
            }
            streamWriter.WriteLine();
            

            streamWriter.Write($"\t\t");
            for (int i = 0; i < permutation.Length; i++)
            {
                streamWriter.Write(permutation[i] + "\t");
            }

            streamWriter.WriteLine();
            streamWriter.WriteLine(unscrambledText.ToString());

            streamWriter.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            streamWriter.Close();
        }

        static void SkipLines(StreamReader reader, int number)
        {
            while (number > 0)
            {
                reader.ReadLine();
                number--;
            }
        }

        static void Main(string[] args)
        {
            string finName = "1.Scrambled.txt";
            string foutName = "Output.txt";
            Processing(finName, foutName);
        }
    }
}