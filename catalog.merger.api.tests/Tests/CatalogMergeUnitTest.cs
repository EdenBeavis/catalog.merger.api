using catalog.merger.api.Features.CatalogMerger.Handlers;
using catalog.merger.api.Features.CatalogMerger.Models;
using catalog.merger.api.tests.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace catalog.merger.api.tests.Tests
{
    public class CatalogMergeUnitTest : IClassFixture<CatalogMergerTestFixture>
    {
        private readonly CatalogMergerTestFixture _fixture;

        public CatalogMergeUnitTest(CatalogMergerTestFixture fixture)
        {
            _fixture = fixture;
        }

        #region When Merging A Catalog...

        [Fact]
        public async Task And_The_Company_List_Is_Empty_Then_Return_A_BadRequst()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = Enumerable.Empty<string>()
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = $"Comapanies list is empty.";
            var response = stringContent;

            message.StatusCode.Should().BeEquivalentTo(HttpStatusCode.BadRequest);
            response.Should().NotBeNullOrEmpty();
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task And_One_Company_Is_Missing_Barcode_Data_Then_Make_A_Catalog_With_Remaining_Company()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = new string[]
                {
                    TestConstants.CompanyNames.NoBarcodeResult,
                    TestConstants.CompanyNames.SuccessResultCompanyB,
                }
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = _fixture.GetSuccessCatalogB();
            var response = JsonConvert.DeserializeObject<IEnumerable<CatalogItemDto>>(stringContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            message.EnsureSuccessStatusCode();
            message.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task And_One_Company_Is_Missing_Supplier_Data_Then_Make_A_Catalog_With_Remaining_Company()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = new string[]
                {
                    TestConstants.CompanyNames.NoSupplierResult,
                    TestConstants.CompanyNames.SuccessResultCompanyA,
                }
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = _fixture.GetSuccessCatalogA();
            var response = JsonConvert.DeserializeObject<IEnumerable<CatalogItemDto>>(stringContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            message.EnsureSuccessStatusCode();
            message.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task And_One_Company_Is_Missing_Catalog_Data_Then_Make_A_Catalog_With_Remaining_Company()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = new string[]
                {
                    TestConstants.CompanyNames.NoCatalogResult,
                    TestConstants.CompanyNames.SuccessResultCompanyB,
                }
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = _fixture.GetSuccessCatalogB();
            var response = JsonConvert.DeserializeObject<IEnumerable<CatalogItemDto>>(stringContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            message.EnsureSuccessStatusCode();
            message.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task And_Data_Is_Correct_Then_Make_A_Catalog_With_All_Companies()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = new string[]
                {
                    TestConstants.CompanyNames.SuccessResultCompanyA,
                    TestConstants.CompanyNames.SuccessResultCompanyB,
                }
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = _fixture.MergedCatalog();
            var response = JsonConvert.DeserializeObject<IEnumerable<CatalogItemDto>>(stringContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            message.EnsureSuccessStatusCode();
            message.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task And_Data_Has_All_Overlapping_Barcodes_Then_Make_A_Catalog_With_Only_First_Company()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                CompanyNames = new string[]
                {
                    TestConstants.CompanyNames.OverlappingCompanyA,
                    TestConstants.CompanyNames.OverlappingCompanyB
                }
            };

            // Act
            var (message, stringContent) = await _fixture.PostMergeRequest(request);

            //Assert
            var expected = _fixture.OverlapCatlog();
            var response = JsonConvert.DeserializeObject<IEnumerable<CatalogItemDto>>(stringContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            message.EnsureSuccessStatusCode();
            message.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().BeEquivalentTo(expected);
        }

        #endregion When Merging A Catalog...
    }
}