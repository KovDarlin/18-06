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
                .SelectMany(line => line.Split(' ', ',', '.'))
                .Where(str => int.TryParse(str, out _))
                .Select(int.Parse)
                .ToList();

            if (numbers.Count == 0)
            {
                Console.WriteLine("File is empty!");
                return;
            }

            var lis = Subsequence(numbers);

            Console.WriteLine($"Length of the largest increasing subsequence: {lis.Count}");
            Console.WriteLine("Subsequence: " + string.Join(" ", lis));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static List<int> Subsequence(List<int> sequence)
    {
        int n = sequence.Count;
        int[] dp = new int[n];
        int[] prev = new int[n];
        Array.Fill(dp, 1);
        Array.Fill(prev, -1);

        int maxLength = 1;
        int maxIndex = 0;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (sequence[i] > sequence[j] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    prev[i] = j;
                }
            }

            if (dp[i] > maxLength)
            {
                maxLength = dp[i];
                maxIndex = i;
            }
        }

        List<int> lis = new();
        for (int i = maxIndex; i >= 0; i = prev[i])
        {
            lis.Add(sequence[i]);
            if (prev[i] == -1) break;
        }
        lis.Reverse();
        return lis;
    }
}
