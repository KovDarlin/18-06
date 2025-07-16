using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter path to file: ");
        string path = Console.ReadLine();

        try
        {
            List<int> numbers = File.ReadAllLines(path)
                .AsParallel()
                .SelectMany(line => line.Split(' ', ',', ';'))
                .Where(str => int.TryParse(str, out _))
                .Select(int.Parse)
                .ToList();

            int maxLength = 0;
            int currentLength = 0;
            List<int> currentSequence = new();
            List<int> longestSequence = new();

            foreach (int number in numbers)
            {
                if (number > 0)
                {
                    currentLength++;
                    currentSequence.Add(number);

                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        longestSequence = new List<int>(currentSequence);
                    }
                }
                else
                {
                    currentLength = 0;
                    currentSequence.Clear();
                }
            }

            Console.WriteLine($"Length of the largest positive sequence: {maxLength}");
            Console.WriteLine("Sequence: " + string.Join(" ", longestSequence));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
