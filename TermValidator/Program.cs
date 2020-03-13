using System;
using System.Diagnostics;
using System.IO;

namespace TermValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "";
			
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var fileStream = new FileStream(path, FileMode.Open))
            using(var reader = new StreamReader(fileStream))
            {
                var families = new ExcelTermFamilyLoader().LoadTermFamilies();

                var problems = new ValidateTerm(new TermFamilyCollection(families, true)).ValidateText(reader);

                stopwatch.Stop();

                foreach (var problem in problems)
                {
                    Console.WriteLine($"Problem with term {problem.Value} in line {problem.ProblemLine}. Suggestion: {problem.Term.PositiveExample}");
                }
            }

            Console.WriteLine($"Needed {stopwatch.Elapsed.TotalSeconds}s");
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
