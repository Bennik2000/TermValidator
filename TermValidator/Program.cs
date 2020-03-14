using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TermValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(Properties.Resources.ResourceManager.GetString("HelpText"));
                Environment.Exit(-1);
                return;
            }

            var excelFile = args[0];
            var pathToValidate = args[1];

            if (!File.Exists(excelFile))
            {
                Console.WriteLine("Excel file not found!");
                Environment.Exit(-1);
                return;
            }

            if (!File.Exists(pathToValidate))
            {
                Console.WriteLine("File to validate not found!");
                Environment.Exit(-1);
                return;
            }
			
            var stopwatch = new Stopwatch();
            List<ValidationProblem> problems = null;



            using (var fileStream = new FileStream(pathToValidate, FileMode.Open))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                var familyLoader = new ExcelTermFamilyLoader(excelFile);

                TermFamilyCollection families;

                try
                {
                    families = familyLoader.LoadTermFamilies();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to load excel file!");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(-1);
                    return;
                }
                

                stopwatch.Start();
                
                problems = new ValidateTerm(families).ValidateText(reader, false).ToList();

                stopwatch.Stop();
            }

            problems = problems.Where(p => p.Term.Classification == Classification.Forbidden).ToList();

            foreach (var problem in problems)
            {
                if (problem.Term.Classification == Classification.Forbidden)
                    Console.WriteLine($"Problem with term {problem.Value} in line {problem.ProblemLine}. Suggestion: {problem.Term.PositiveExample}");
            }

            Console.WriteLine($"Found {problems.Count} Problems!");
            Console.WriteLine($"Needed {stopwatch.Elapsed.TotalSeconds}s");

            if (problems.Count != 0)
            {
                Environment.Exit(-1);
            }
        }
    }
}
