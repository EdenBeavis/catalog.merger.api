using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace catalog.merger.api.Infrastructure.Helper
{
    public static class CSVHelper
    {
        public static async IAsyncEnumerable<T> ReadFromCSV<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csvReader.GetRecordsAsync<T>();

            await foreach (var record in records) yield return record;
        }
    }
}