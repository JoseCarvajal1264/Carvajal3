using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using Carvajal3.Models;
using Carvajal3.Services;

namespace Carvajal3.Views;

public partial class BuscadorPage : ContentPage
{
    private readonly CountriesDatabase _database;

    public BuscadorPage()
    {
        InitializeComponent();
        _database = new CountriesDatabase();
    }

    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        string countryName = searchEntry.Text;

        if (string.IsNullOrWhiteSpace(countryName))
        {
            await DisplayAlert("Error", "Por favor ingresa el nombre de un país.", "OK");
            return;
        }

        try
        {
            // Llamada a la API
            using HttpClient client = new();
            string url = $"https://restcountries.com/v3.1/name/{countryName}?fields=name,region,maps";
            var countries = await client.GetFromJsonAsync<List<Country>>(url);

            if (countries?.Count > 0)
            {
                var country = countries.First();

                // Guardar en SQLite
                await _database.SaveCountryAsync(new CountryDb
                {
                    name = country.name.Common,
                    region = country.region,
                    maps = country.maps.GoogleMaps,
                });

                resultLabel.Text = $"País encontrado: {country.name.Common}\nRegión: {country.region}\nLink: {country.maps.GoogleMaps}";
            }
            else
            {
                resultLabel.Text = "No se encontró ningún país con ese nombre.";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al buscar el país: {ex.Message}", "OK");
        }
    }

    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        searchEntry.Text = string.Empty;
        resultLabel.Text = string.Empty;
    }
}
