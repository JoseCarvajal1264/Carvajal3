using Carvajal3.Models;
using Carvajal3.Services;

namespace Carvajal3.Views;

public partial class ListadoPage : ContentPage
{
    private readonly CountriesDatabase _database;

    public ListadoPage()
    {
        InitializeComponent();
        _database = new CountriesDatabase();
        LoadCountries();
    }

   

    private async void LoadCountries()
    {
        var countries = await _database.GetCountriesAsync();
        var displayData = countries.Select(c => new { DisplayText = $"Nombre País: {c.name}, Región: {c.region}, Link: {c.maps}" }).ToList();
        countriesListView.ItemsSource = displayData;
    }
}
    