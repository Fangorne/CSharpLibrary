using Xunit;

namespace GeoServices;

public class GeoTests
{
    [Fact]
    public void LocalizationTests()
    {
        var geoLocalizationService = new GeoLocalizationService();

        var coordinates = geoLocalizationService.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
        Assert.Equal(2.49134, coordinates.features[0].geometry.coordinates[0], 5);
        Assert.Equal(48.71088, coordinates.features[0].geometry.coordinates[1], 5);
        coordinates = geoLocalizationService.GeoLocalizeAsync("Soisy").WaitAsync(CancellationToken.None).Result;
        Assert.Equal(2.30165, coordinates.features[0].geometry.coordinates[0], 5);
        Assert.Equal(48.98868, coordinates.features[0].geometry.coordinates[1], 5);
    }
}