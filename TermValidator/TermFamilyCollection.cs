using System;
using System.Collections.Generic;
using System.Linq;

namespace TermValidator
{
    class TermFamilyCollection
    {
        public readonly IReadOnlyCollection<TermFamily> TermFamilies; 

        public TermFamilyCollection(IReadOnlyCollection<TermFamily> termFamilies)
        {
            TermFamilies = termFamilies;
        }

        public (TermFamily family, Term term) SearchMatchingTermFamily(string term, bool caseSensitive)
        {
            term = term.Trim();

            Term matchingTerm = null;

            var termFamily = TermFamilies.FirstOrDefault(f => 
                f.Terms.Any(t =>
                {
                    var isMatch = t.Value.Equals(term,
                        caseSensitive
                            ? StringComparison.CurrentCulture
                            : StringComparison.CurrentCultureIgnoreCase);

                    if (isMatch) matchingTerm = t;

                    return isMatch;
                }));

            return (termFamily, matchingTerm);
        }
    }
}
