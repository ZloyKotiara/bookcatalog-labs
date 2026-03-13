using System.Net;

namespace BookCatalog.Web.Tests;

public class BooksPageTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BooksPageTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HomePage_or_BooksPage_should_open_successfully()
    {
        var response = await _client.GetAsync("/Books");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Каталог книг", content);
    }
}
