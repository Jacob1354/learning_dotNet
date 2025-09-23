using System.Runtime.InteropServices;

namespace System;
class Program
{
    static void Main(string[] args)
    {

        bool running = true;
        var x = 200;
        var lines = ReadFrom("sampleQuotes.txt");
        new Task(async () =>
        {
            foreach (string line in lines)
            {
                Console.Write(line);
                if (!string.IsNullOrWhiteSpace(line))
                {
                    await Task.Delay(x);
                }
            }
            running = false;
        }).Start();
        new Task(async () =>
        {
            while (running)
            {
                ConsoleKeyInfo c = Console.ReadKey(false);
                if (c.Key == ConsoleKey.UpArrow)
                    x = x - 50 < 1 ? 10 : x - 50;
                else
                    x += 50;
                await Task.Delay(30);
            }
        }).Start();
        while (running) ;
    }
    
    static IEnumerable<string> ReadFrom(string file)
    {
        string? line;
        using (var reader = File.OpenText(file))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var words = line.Split(' ');
                foreach (var word in words)
                {
                    yield return word + " ";
                }
                yield return Environment.NewLine;
            }
        }
    }
}