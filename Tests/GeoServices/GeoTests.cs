using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xunit;

namespace GeoServices
{
    public class GeoTests
    {
        [Fact]
        public async Task Way()
        {
            GeoLocalization geoLocalization = new GeoLocalization();

            var coordinates = geoLocalization.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
            coordinates = geoLocalization.GeoLocalizeAsync("Soisy").WaitAsync(CancellationToken.None).Result;
            Assert.Equal(2.30165, coordinates.features[0].geometry.coordinates[0], 5);
            Assert.Equal(48.98868, coordinates.features[0].geometry.coordinates[1], 5);
        }
    }
}
