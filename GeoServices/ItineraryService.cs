using System.Globalization;
using System.Net.Http.Headers;
using GeoCoordinatePortable;
using GeoServices.Model;
using Newtonsoft.Json;

namespace GeoServices;

public class ItineraryService
{
    public async Task<Itinerary?> ComputeItinerary(GeoCoordinate firstCoordinates, GeoCoordinate secondCoordinates)
    {
        if (firstCoordinates == null) throw new ArgumentNullException(nameof(firstCoordinates));
        if (secondCoordinates == null) throw new ArgumentNullException(nameof(secondCoordinates));

        // Create a new product
        var product = new ItineraryRequest
        {
            resource = "bdtopo-osrm",
            start = $"{firstCoordinates.Latitude.ToString(CultureInfo.InvariantCulture)},{firstCoordinates.Longitude.ToString(CultureInfo.InvariantCulture)}",
            end = $"{secondCoordinates.Latitude.ToString(CultureInfo.InvariantCulture)},{firstCoordinates.Longitude.ToString(CultureInfo.InvariantCulture)}",
            profile = "car",
            optimization = "fastest",
            distanceUnit = "meter",
            timeUnit = "minute"
        };
        var json = JsonConvert.SerializeObject(product);
        using var httpSocketHandler = new SocketsHttpHandler();
        using (var httpClient = new HttpClient(httpSocketHandler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"),
                       "https://wxs.ign.fr/calcul/geoportail/itineraire/rest/1.0.0/route"))
            {
                request.Headers.TryAddWithoutValidation("accept", "application/json");

                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await httpClient.SendAsync(request);

                var responseInJson = await response.Content.ReadAsStringAsync();

                var root = JsonConvert.DeserializeObject<Itinerary>(responseInJson);

                return root;
            }
        }
    }
}