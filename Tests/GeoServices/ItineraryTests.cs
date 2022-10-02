using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServices.Tests
{
    public class ItineraryTests
    {
        [Fact]
        public void Itinerary()
        {
            GeoLocalization geoLocalization = new GeoLocalization();
            Itinerary itinerary = new Itinerary();
            var coordinates = geoLocalization.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
            var firstCoordinates =
                $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";

            coordinates = geoLocalization.GeoLocalizeAsync("Soisy").WaitAsync(CancellationToken.None).Result;
            var secondCoordinates =
                $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";



            var result = itinerary.ComputeItinerary(firstCoordinates, secondCoordinates).WaitAsync(CancellationToken.None).Result;
        }

    }
}
