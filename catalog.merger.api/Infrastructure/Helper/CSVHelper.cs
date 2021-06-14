using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace catalog.merger.api.Infrastructure.Helper
{
    public static class CSVHelper
    {
        public static async Task WriteToCsv<T>(IEnumerable<T> records, string filePath, CancellationToken cancellationToken)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            await csv.WriteRecordsAsync(records, cancellationToken);
        }

        public static async IAsyncEnumerable<T> ReadFromCSV<T>(string filePath, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var reader = new StreamReader(filePath);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csvReader.GetRecordsAsync<T>(cancellationToken).WithCancellation(cancellationToken);

            await foreach (var record in records) yield return record;
        }
    }
}