
using System.Globalization;
using System.Web;
using Newtonsoft.Json;

namespace GeoServices
{
    public class GeoLocalization
    {
        public RootAddress? GeoLocalize(string address)
        {
            return GeoLocalizeAsync(address).WaitAsync(CancellationToken.None).Result;
        }

        public async Task<RootAddress?> GeoLocalizeAsync(string address)
        {
            // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using var httpClient = new HttpClient();
            var addressEncoded = HttpUtility.UrlEncode(address);
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), 
                       $"https://api-adresse.data.gouv.fr/search/?q={addressEncoded}&limit=1"))
            {
                var response = await httpClient.SendAsync(request);

                var responseInJson = await response.Content.ReadAsStringAsync();

                var root = JsonConvert.DeserializeObject<RootAddress>(responseInJson);
 
                return root;
            }
        }
    }
}
