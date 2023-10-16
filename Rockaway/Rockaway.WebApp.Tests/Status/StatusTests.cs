using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Rockaway.WebApp.Services;

public class StatusTests {

	[Fact]
	public async Task Homepage_Works() {
		var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var result = await client.GetAsync("/status");
		result.EnsureSuccessStatusCode();
	}

	private static ServerStatus fakeStatus = new() {
		Assembly = "TEST_ASSEMBLY",
		DateTime = "DATETIME",
		Hostname = "HOSTNAME",
		Modified = "MODIFIED"
	};

	private static ServerRestartStatus fakeRestartStatus = new() {
		TimeSinceLastRestart = "15"
	};

	private class FakeStatusReporter : IStatusReporter {
		public ServerStatus GetStatus() => fakeStatus;
		public ServerRestartStatus GetTimeSinceLastRestart() => fakeRestartStatus;
	}

	private static readonly JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web);

	[Fact]
	public async Task Status_Returns_Correct_Json() {
		var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
			builder.ConfigureServices(
				services => services.AddSingleton<IStatusReporter>(new FakeStatusReporter())
			)
		);
		var client = factory.CreateClient();
		var json = await client.GetStringAsync("/status");
		var status = JsonSerializer.Deserialize<ServerStatus>(json, jsonSerializerOptions);
		status.ShouldNotBeNull();
		status.ShouldBeEquivalentTo(fakeStatus);
	}

	[Fact]
	public async Task Restart_Status_Returns_Correct_Json() {
		var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
				builder.ConfigureServices(
				services => services.AddSingleton<IStatusReporter>(new FakeStatusReporter())
			)
		);
		var client = factory.CreateClient();
		var json = await client.GetStringAsync("/uptime");
		var status = JsonSerializer.Deserialize<ServerRestartStatus>(json, jsonSerializerOptions);
		status.ShouldNotBeNull();
		status.ShouldBeEquivalentTo(fakeRestartStatus);
	}
}
