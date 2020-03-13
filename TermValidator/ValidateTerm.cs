using System.Collections.Generic;
using System.IO;

namespace TermValidator
{
    class ValidateTerm
    {
        public readonly TermFamilyCollection TermFamilyCollection;

        public ValidateTerm(TermFamilyCollection termFamilyCollection)
        {
            TermFamilyCollection = termFamilyCollection;
        }

        public IEnumerable<ValidationProblem> ValidateText(TextReader textReader)
        {
            var problems = new List<ValidationProblem>();

            string line;
            int lineNumber = 0;
            while ((line = textReader.ReadLine()) != null)
            {
                var problemsInLine = ValidateLine(line, lineNumber);

                problems.AddRange(problemsInLine);

                lineNumber++;
            }

            return problems;
        }

        private IEnumerable<ValidationProblem> ValidateLine(string line, int lineNumber)
        {
            var problems = new List<ValidationProblem>();

            var words = line.Split(' ');

            foreach (var word in words)
            {
                var matchingResult = TermFamilyCollection.SearchMatchingTermFamily(word);
                
                if(matchingResult.family == null) continue;

                problems.Add(new ValidationProblem(
                    matchingResult.family, 
                    matchingResult.term,
                    word, 
                    0, 
                    0, 
                    lineNumber));
            }

            return problems;
        }
    }

    class ValidationProblem
    {
        public readonly int ProblemLine;
        public readonly int ProblemStart;
        public readonly int ProblemEnd;

        public readonly string Value;

        public readonly TermFamily TermFamily;
        public readonly Term Term;

        public ValidationProblem(TermFamily termFamily, Term term, string value, int problemStart, int problemEnd, int problemLine)
        {
            TermFamily = termFamily;
            Term = term;
            Value = value;
            ProblemStart = problemStart;
            ProblemEnd = problemEnd;
            ProblemLine = problemLine;
        }
    }
}
