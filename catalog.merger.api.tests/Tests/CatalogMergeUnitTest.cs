using catalog.merger.api.Features.CatalogMerger.Handlers;
using catalog.merger.api.tests.Fixtures;
using FluentAssertions;
using System.Collections.Generic;
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

        #region When merging a catalog and the company list is empty

        [Fact]
        public async Task Then_Return_A_BadRequst()
        {
            // Arrange
            var request = new CatalogMergeRequest
            {
                Companies = new List<string>()
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

        #endregion When merging a catalog and the company list is empty
    }
}