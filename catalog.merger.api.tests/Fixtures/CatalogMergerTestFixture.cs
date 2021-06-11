using catalog.merger.api.Features.CatalogMerger.Handlers;
using catalog.merger.api.tests.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace catalog.merger.api.tests.Fixtures
{
    public class CatalogMergerTestFixture : TestWebApplicationFactory
    {
        private const string MergerApi = "api/v1/catalogmerger";

        public async Task<(HttpResponseMessage message, string stringContent)> PostMergeRequest(CatalogMergeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(MergerApi, stringContent);
            var responseString = await response.Content.ReadAsStringAsync();

            return (response, responseString);
        }
    }
}