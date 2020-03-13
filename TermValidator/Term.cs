using System.Collections.Generic;

namespace TermValidator
{
    class TermFamily
    {
        public List<Term> Terms;

        public TermFamily(List<Term> terms)
        {
            Terms = terms;
        }
    }

    class Term
    {
        public readonly string Value;
        public readonly Classification Classification;
        public readonly string PositiveExample;
        public readonly string NegativeExample;

        public Term(
            string value, Classification classification, 
            string positiveExample, string negativeExample)
        {
            Value = value;
            Classification = classification;
            PositiveExample = positiveExample;
            NegativeExample = negativeExample;
        }
    }

    enum Classification
    {
        Allowed,
        Forbidden,
    }
}
