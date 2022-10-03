using System.Diagnostics;
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
        var firstCoordinates = geoLocalizationService.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
        var secondCoordinates = geoLocalizationService.GeoLocalizeAsync("Versailles").WaitAsync(CancellationToken.None).Result;

        Assert.NotNull(firstCoordinates);
        Assert.NotNull(secondCoordinates);
        var firstGeoCoordinate = geoLocalizationService.GeoAddressToGeoCoordinate(firstCoordinates).ToList();
        var secondGeoCoordinate = geoLocalizationService.GeoAddressToGeoCoordinate(secondCoordinates).ToList();

        Assert.NotNull(firstGeoCoordinate);
        Assert.NotNull(secondGeoCoordinate);
        Assert.NotEmpty(firstGeoCoordinate);
        Assert.NotEmpty(secondGeoCoordinate);

        var result = itineraryService.ComputeItinerary(
                firstGeoCoordinate.First(),
                secondGeoCoordinate.First())
            .WaitAsync(CancellationToken.None).Result;

        Assert.NotNull(result);
        Assert.Equal("meter", result.distanceUnit);
        Assert.Equal(37085.4, result.distance, 1);
        Assert.Equal(34.47, result.duration, 2);
        Assert.Equal("minute", result.timeUnit);
    }
}