using System;
using System.Collections.Generic;
using System.Linq;

namespace TermValidator
{
    class TermFamilyCollection
    {
        public readonly IReadOnlyCollection<TermFamily> TermFamilies;
        public readonly bool CaseSensitive;

        public TermFamilyCollection(IReadOnlyCollection<TermFamily> termFamilies, bool caseSensitive)
        {
            TermFamilies = termFamilies;
            CaseSensitive = caseSensitive;
        }

        public (TermFamily family, Term term) SearchMatchingTermFamily(string term)
        {
            term = term.Trim();

            Term matchingTerm = null;

            var termFamily = TermFamilies.FirstOrDefault(f => 
                f.Terms.Any(t =>
                {
                    var isMatch = t.Value.Equals(term,
                        CaseSensitive
                            ? StringComparison.CurrentCulture
                            : StringComparison.CurrentCultureIgnoreCase);

                    if (isMatch) matchingTerm = t;

                    return isMatch;
                }));

            return (termFamily, matchingTerm);
        }
    }
}
