using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace QuadiroApi
{
	/// <summary>
	/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
	/// </summary>
	public class LocalEntryPoint
	{
		private static string _dsn = string.Empty;

		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
			_dsn = config.GetSection("Sentry:Dsn").Value;
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
		   WebHost.CreateDefaultBuilder(args)
			   .UseStartup<Startup>()
			   .Build();
	}
}