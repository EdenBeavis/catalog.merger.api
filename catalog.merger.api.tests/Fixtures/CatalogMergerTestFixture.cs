using catalog.merger.api.Features.CatalogMerger.Handlers;
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.tests.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace catalog.merger.api.tests.Fixtures
{
    public class CatalogMergerTestFixture : TestWebApplicationFactory
    {
        private const string MergerApi = "api/v1/catalogmerger/merge";

        public async Task<(HttpResponseMessage message, string stringContent)> PostMergeRequest(CatalogMergeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(MergerApi, stringContent);
            var responseString = await response.Content.ReadAsStringAsync();

            return (response, responseString);
        }

        public IEnumerable<CatalogItemDto> GetSuccessCatalogA() => new List<CatalogItemDto>
        {
            GetCatalogItemDto("SKU-Company-A-1", "Potato cakes", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-2", "Jerky", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-3", "Cakes", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-AB-1", "Banana", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-4", "Donut", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-AB-2", "Potato", TestConstants.CompanyNames.SuccessResultCompanyA)
        };

        public IEnumerable<CatalogItemDto> GetSuccessCatalogB() => new List<CatalogItemDto>
        {
            GetCatalogItemDto("SKU-Company-B-1", "Potato cakes salted", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-B-2", "Jerky Spicy", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-B-3", "Cakes", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-AB-1", "Banana", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-A-4", "Grilled Cheese", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-AB-2", "Potato b", TestConstants.CompanyNames.SuccessResultCompanyB)
        };

        public IEnumerable<CatalogItemDto> MergedCatalog() => new List<CatalogItemDto>
        {
            GetCatalogItemDto("SKU-Company-A-1", "Potato cakes", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-2", "Jerky", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-3", "Cakes", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-AB-1", "Banana", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-A-4", "Donut", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-AB-2", "Potato", TestConstants.CompanyNames.SuccessResultCompanyA),
            GetCatalogItemDto("SKU-Company-B-1", "Potato cakes salted", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-B-2", "Jerky Spicy", TestConstants.CompanyNames.SuccessResultCompanyB),
            GetCatalogItemDto("SKU-Company-B-3", "Cakes", TestConstants.CompanyNames.SuccessResultCompanyB),
        };

        public IEnumerable<CatalogItemDto> OverlapCatlog() => new List<CatalogItemDto>
        {
            GetCatalogItemDto("SKU-Company-A-1", "Potato cakes", TestConstants.CompanyNames.OverlappingCompanyA),
            GetCatalogItemDto("SKU-Company-A-2", "Jerky", TestConstants.CompanyNames.OverlappingCompanyA),
            GetCatalogItemDto("SKU-Company-A-3", "Cakes", TestConstants.CompanyNames.OverlappingCompanyA),
            GetCatalogItemDto("SKU-Company-A-4", "Banana", TestConstants.CompanyNames.OverlappingCompanyA),
            GetCatalogItemDto("SKU-Company-A-5", "Donut", TestConstants.CompanyNames.OverlappingCompanyA),
            GetCatalogItemDto("SKU-Company-A-6", "Potato", TestConstants.CompanyNames.OverlappingCompanyA)
        };

        private static CatalogItemDto GetCatalogItemDto(string SKU, string Description, string source) => new CatalogItemDto
        {
            SKU = SKU,
            Description = Description,
            Source = source
        };
    }
}