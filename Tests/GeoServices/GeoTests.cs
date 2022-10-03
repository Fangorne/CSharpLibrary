using Xunit;

namespace GeoServices;

public class GeoTests
{
    [Fact]
    public void LocalizationTests()
    {
        var geoLocalizationService = new GeoLocalizationService();

        var coordinates = geoLocalizationService.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
        Assert.NotNull(coordinates);
        Assert.Equal(2.49134, coordinates.features[0].geometry.coordinates[0], 5);
        Assert.Equal(48.71088, coordinates.features[0].geometry.coordinates[1], 5);

        coordinates = geoLocalizationService.GeoLocalizeAsync("Versailles").WaitAsync(CancellationToken.None).Result;
        Assert.NotNull(coordinates);
        Assert.Equal(2.13132, coordinates.features[0].geometry.coordinates[0], 5);
        Assert.Equal(48.80302, coordinates.features[0].geometry.coordinates[1], 5);
    }
}