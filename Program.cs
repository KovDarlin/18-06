using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter path to file:");
        string path = Console.ReadLine();

        try
        {
            List<int> num = File.ReadAllLines(path)
                .SelectMany(line => line.Split(' ', ',', '.'))
                .Where(str => int.TryParse(str, out _))
                .Select(int.Parse)
                .ToList();

            int Counting = num.AsParallel().Distinct().Count();
            Console.WriteLine($"Unique: {Counting}");
        }
        catch(Exception problem)
        {
            Console.WriteLine("Error: " + problem.Message);
        }
    }
}