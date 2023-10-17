namespace Rockaway.WebApp.Tests.Pages;

public class PageTests {

	[Fact]
	public async Task Homepage_Works() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync("/");
		result.EnsureSuccessStatusCode();
	}

	[Theory]
	[InlineData("/")]
	[InlineData("/privacy")]
	// [InlineData("/contact")]
	public async Task Page_Works(string url) {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync(url);
		result.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task Homepage_Title_Has_Correct_Content() {
		var browsingContext = BrowsingContext.New(Configuration.Default);
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var html = await client.GetStringAsync("/");
		var dom = await browsingContext.OpenAsync(req => req.Content(html));
		var title = dom.QuerySelector("title");
		title.ShouldNotBeNull();
		title.InnerHtml.ShouldBe("Rockaway");
	}

	[Theory]
	[InlineData("/", "Rockaway")]
	[InlineData("/privacy", "Privacy Policy - Rockaway")]
	[InlineData("/contact", "Contact Us - Rockaway")]
	public async Task Page_Has_Correct_Title(string url, string title) {
		var browsingContext = BrowsingContext.New(Configuration.Default);
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var html = await client.GetStringAsync(url);
		var dom = await browsingContext.OpenAsync(req => req.Content(html));
		var titleElement = dom.QuerySelector("title");
		titleElement.ShouldNotBeNull();
		titleElement.InnerHtml.ShouldBe(title);
	}


}