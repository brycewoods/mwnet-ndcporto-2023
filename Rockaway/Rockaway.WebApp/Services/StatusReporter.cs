using System.Reflection;

namespace Rockaway.WebApp.Services;

public class StatusReporter : IStatusReporter {
	private static readonly Assembly assembly = Assembly.GetEntryAssembly()!;
	public ServerStatus GetStatus() => new() {
		Assembly = assembly.FullName ?? "Assembly.GetEntryAssembly() returned null",
		Modified = new DateTimeOffset(File.GetLastWriteTimeUtc(assembly.Location), TimeSpan.Zero).ToString("O"),
		Hostname = Environment.MachineName,
		DateTime = DateTimeOffset.UtcNow.ToString("O"),
		Uptime = new DateTimeOffset(Environment.TickCount, TimeSpan.Zero).ToString("hh:mm:ss")
	};

	public ServerRestartStatus GetTimeSinceLastRestart() => new() {
		TimeSinceLastRestart = new DateTimeOffset(Environment.TickCount, TimeSpan.Zero).ToString("ss")
	};
}