using System.Web;
using GeoServices.Model;
using Newtonsoft.Json;
using GeoCoordinatePortable;

namespace GeoServices;

public class GeoLocalizationService
{
    public IEnumerable<GeoCoordinate?> GeoAddressToGeoCoordinate(GeoAddress geoAddress)
    {
        if (geoAddress == null) throw new ArgumentNullException(nameof(geoAddress));

        foreach (var geoAddressFeature in geoAddress.features)
        {
            yield return new GeoCoordinate(geoAddressFeature.geometry.coordinates[0], geoAddressFeature.geometry.coordinates[1]);
        }
    }

    public GeoAddress? GeoLocalize(string address, int maxAddress = 1, bool autoCompletion = true)
    {
        return GeoLocalizeAsync(address, maxAddress, autoCompletion).WaitAsync(CancellationToken.None).Result;
    }

    public async Task<GeoAddress?> GeoLocalizeAsync(string address, int maxAddress = 1, bool autoCompletion = true)
    {
        using var httpSocketHandler = new SocketsHttpHandler();
        using var httpClient = new HttpClient(httpSocketHandler);
        var addressEncoded = HttpUtility.UrlEncode(address);
        using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                   $"https://api-adresse.data.gouv.fr/search/?q={addressEncoded}&limit={maxAddress}&autocomplete={Convert.ToInt32(autoCompletion)}"))
        {
            var response = await httpClient.SendAsync(request);

            var responseInJson = await response.Content.ReadAsStringAsync();

            var root = JsonConvert.DeserializeObject<GeoAddress>(responseInJson);

            return root;
        }
    }
}