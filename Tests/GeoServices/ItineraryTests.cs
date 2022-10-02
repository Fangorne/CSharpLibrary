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

        Assert.Equal("meter", result.distanceUnit);
        Assert.Equal(50601.90000000000, result.distance, 1);
        Assert.Equal(38.3117, result.duration, 4);
        Assert.Equal("minute", result.timeUnit);


        //MyLogger.Information($"Distance {root.distance} {root.distanceUnit} Durée {root.duration} {root.timeUnit}");

        //foreach (var step in root.portions[0].steps)
        //{
        //    MyLogger.Information($"{step.instruction} {step.attributes.name} Distance : {step.distance} Durée : {step.duration}");
        //}
    }
}