using System.Collections.Generic;

namespace TermValidator
{
    class DummyTermFamilyLoader : ITermFamilyLoader
    {
        public List<TermFamily> LoadTermFamilies()
        {
            return new List<TermFamily>()
            {
                /*new TermFamily(new List<Term>()
                {
                    new Term("ich", Classification.Forbidden, "Nicht ich verwenden!"),
                    new Term("man", Classification.Forbidden, "Nicht man verwenden!"),
                    new Term("sie", Classification.Forbidden, "Nicht sie verwenden!"),
                }),
                new TermFamily(new List<Term>()
                {
                    new Term("einige", Classification.Forbidden, "Nicht einige verwenden!"),
                    new Term("manchmal", Classification.Forbidden, "Nicht manchmal verwenden!"),
                }),
                new TermFamily(new List<Term>()
                {
                    new Term("einfach", Classification.Forbidden, "Die Lösung des Problems erweist sich als unkompliziert."),
                    new Term("problemlos", Classification.Allowed, "Die Lösung des Problems erweist sich als unkompliziert."),
                    new Term("ohne Schwierigkeiten", Classification.Allowed, "Die Lösung des Problems erweist sich als unkompliziert."),
                }),
                new TermFamily(new List<Term>()
                {
                    new Term("wichtig", Classification.Forbidden, ""),
                    new Term("bedeutsam", Classification.Allowed, ""),
                    new Term("erheblich", Classification.Allowed, ""),
                }),
                new TermFamily(new List<Term>()
                {
                    new Term("welcher", Classification.Forbidden, ""),
                    new Term("welches", Classification.Forbidden, ""),
                    new Term("welche", Classification.Forbidden, ""),
                    new Term("der", Classification.Allowed, ""),
                    new Term("die", Classification.Allowed, ""),
                    new Term("das", Classification.Allowed, ""),
                }),
                new TermFamily(new List<Term>()
                {
                    new Term("natürlich", Classification.Forbidden, ""),
                    new Term("logischerweise", Classification.Forbidden, ""),
                    new Term("selbstverständlich", Classification.Forbidden, ""),
                }),*/
            };
        }
    }
}
