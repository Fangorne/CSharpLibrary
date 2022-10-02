using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Xunit;

namespace GeoServices
{
    public class GeoTests
    {
        [Fact]
        public async Task Way()
        {
            string firstCoordinates, secondCoordinates;
            // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/

            GeoLocalization geoLocalization = new GeoLocalization();

            var coordinates = geoLocalization.GeoLocalizeAsync("Yerres").WaitAsync(CancellationToken.None).Result;
            firstCoordinates =
                $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";
            coordinates = geoLocalization.GeoLocalizeAsync("Soisy").WaitAsync(CancellationToken.None).Result;
            secondCoordinates =
                $"{coordinates.features[0].geometry.coordinates[0].ToString(CultureInfo.InvariantCulture)},{coordinates.features[0].geometry.coordinates[1].ToString(CultureInfo.InvariantCulture)}";

            // Create a new product
            Carto product = new Carto
            {
                resource = "bdtopo-osrm",
                start = firstCoordinates,
                end = secondCoordinates,
                profile = "car",
                optimization = "fastest",
                distanceUnit = "meter",
                timeUnit = "minute"
            };
            var json = JsonConvert.SerializeObject(product);

            // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://wxs.ign.fr/calcul/geoportail/itineraire/rest/1.0.0/route"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "application/json");

                    request.Content = new StringContent(json);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    var responeInJson = await response.Content.ReadAsStringAsync();

                    var root = JsonConvert.DeserializeObject<Root>(responeInJson);

                    MyLogger.Information($"Distance {root.distance} {root.distanceUnit} Durée {root.duration} {root.timeUnit}");

                    foreach (var step in root.portions[0].steps)
                    {
                        MyLogger.Information($"{step.instruction} {step.attributes.name} Distance : {step.distance} Durée : {step.duration}");
                    }
                }
            }
        }
    }
}
