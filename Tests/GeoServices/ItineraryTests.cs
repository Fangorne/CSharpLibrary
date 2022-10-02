using System.Globalization;
using Xunit;

namespace GeoServices.Tests;

public class ItineraryTests
{
    [Fact]
    public void Itinerary()
    {
        var geoLocalizationService = new GeoLocalizationService();
        var itineraryService = new ItineraryService();
        var coordinates = geoLocalizationService.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
        var firstCoordinates =
            $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";

        coordinates = geoLocalizationService.GeoLocalizeAsync("Soisy").WaitAsync(CancellationToken.None).Result;
        var secondCoordinates =
            $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";

        var result = itineraryService.ComputeItinerary(firstCoordinates, secondCoordinates)
            .WaitAsync(CancellationToken.None).Result;
    }
}