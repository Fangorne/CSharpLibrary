using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace GeoServices
{
    public class Itinerary
    {
        public async Task<Root?> ComputeItinerary(string firstCoordinates, string secondCoordinates)
        {
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

                    var responseInJson = await response.Content.ReadAsStringAsync();

                    var root = JsonConvert.DeserializeObject<Root>(responseInJson);

                    return root;
                }
            }
        }
    }
}
