using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TermValidator
{
    class ExcelTermFamilyLoader : ITermFamilyLoader
    {
        private readonly string Path;

        private const string FamilyIdColumn = "FamilyId";
        private const string ValueColumn = "Value";
        private const string ClassificationColumn = "Classification";
        private const string NegativeExampleColumn = "Negative Example";
        private const string PositiveExampleColumn = "Positive Example";


        public ExcelTermFamilyLoader(string path)
        {
            Path = path;
        }


        public List<TermFamily> LoadTermFamilies()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var termFamilies = new Dictionary<string, TermFamily>();

            using (var package = new ExcelPackage(new FileInfo(Path)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var dimension = worksheet.Dimension;
                var columnIds = GetColumnIdsFromHeaders(worksheet);

                for (int i = dimension.Start.Row + 1; i < dimension.End.Row; i++)
                {
                    var familyId = worksheet.Cells[i, columnIds[FamilyIdColumn]].Text;

                    var term = LoadTermFromRow(worksheet, i, columnIds);

                    if(term == null) continue;

                    if (termFamilies.ContainsKey(familyId))
                    {
                        termFamilies[familyId].Terms.Add(term);
                    }
                    else
                    {
                        termFamilies.Add(familyId, new TermFamily(new List<Term>() { term }));
                    }
                }

            }

            return termFamilies.Values.ToList();
        }

        private Dictionary<string, int> GetColumnIdsFromHeaders(ExcelWorksheet worksheet)
        {
            var dimension = worksheet.Dimension;

            var firstRowCells = worksheet.Cells[
                dimension.Start.Row, 
                dimension.Start.Column, 
                dimension.Start.Row,
                dimension.End.Column];

            var familyIdColumn = firstRowCells
                .FirstOrDefault(c => c.Text == FamilyIdColumn)?.Start?.Column;

            var valueColumn = firstRowCells
                .FirstOrDefault(c => c.Text == ValueColumn)?.Start?.Column;

            var classificationColumn = firstRowCells
                .FirstOrDefault(c => c.Text == ClassificationColumn)?.Start?.Column;

            var negativeExampleColumn = firstRowCells
                .FirstOrDefault(c => c.Text == NegativeExampleColumn)?.Start?.Column;

            var positiveExampleColumn = firstRowCells
                .FirstOrDefault(c => c.Text == PositiveExampleColumn)?.Start?.Column;

            var dictionary = new Dictionary<string, int>();

            if (familyIdColumn.HasValue) 
                dictionary.Add(FamilyIdColumn, familyIdColumn.Value);

            if (valueColumn.HasValue) 
                dictionary.Add(ValueColumn, valueColumn.Value);

            if (classificationColumn.HasValue)
                dictionary.Add(ClassificationColumn, classificationColumn.Value);

            if (negativeExampleColumn.HasValue) 
                dictionary.Add(NegativeExampleColumn, negativeExampleColumn.Value);

            if (positiveExampleColumn.HasValue) 
                dictionary.Add(PositiveExampleColumn, positiveExampleColumn.Value);

            return dictionary;
        }

        private Term LoadTermFromRow(ExcelWorksheet worksheet, int row, Dictionary<string, int> columnIds)
        {
            var value = worksheet.Cells[row, columnIds[ValueColumn]].Text;
            var classificationString = worksheet.Cells[row, columnIds[ClassificationColumn]].Text;
            var negativeExample = worksheet.Cells[row, columnIds[NegativeExampleColumn]].Text;
            var positiveExample = worksheet.Cells[row, columnIds[PositiveExampleColumn]].Text;


            if (string.IsNullOrEmpty(value.Trim())) return null;

            Classification classification;

            if (classificationString.Equals(Classification.Allowed.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
            {
                classification = Classification.Allowed;
            }
            else if (classificationString.Equals(Classification.Forbidden.ToString(),
                StringComparison.CurrentCultureIgnoreCase))
            {
                classification = Classification.Forbidden;
            }
            else
            {
                return null;
            }

            return new Term(value, classification, positiveExample, negativeExample);
        }
    }
}