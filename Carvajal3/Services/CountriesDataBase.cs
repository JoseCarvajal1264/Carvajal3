using SQLite;
using Carvajal3.Models;

namespace Carvajal3.Services;

public class CountriesDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public CountriesDatabase()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "countries.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<CountryDb>().Wait();
    }

    public Task<List<CountryDb>> GetCountriesAsync()
    {
        return _database.Table<CountryDb>().ToListAsync();
    }

    public Task<int> SaveCountryAsync(CountryDb country)
    {
        return _database.InsertAsync(country);
    }
}
