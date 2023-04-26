using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using StockBot.Domain.Entities;
using StockBot.Domain.Externals;

namespace StockBot.Infrastructure.Externals;

public class FileManager : IFileManager
{
    public async Task<IEnumerable<Ticker>> GetTickersFromFileAsync(string fileRoute, string fileName)
    {
        var completeFileRoute = Path.Combine(fileRoute, fileName);

        if (!Directory.Exists(fileRoute)) throw new FileLoadException("Cannot load file. Directory not found");
        
        var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
        using var reader = new StreamReader(completeFileRoute);
        using var csv = new CsvReader(reader, config);
        
        return  await csv.GetRecordsAsync<Ticker>().ToListAsync();
    }
}