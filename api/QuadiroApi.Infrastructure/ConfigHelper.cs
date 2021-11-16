using Microsoft.Extensions.Configuration;

namespace QuadiroApi.Infrastructure
{
	public class ConfigHelper
	{
		private static readonly IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

		#region Connection

		public static string ConnectionString
		{
			get
			{
				return config.GetSection("ConnectionString:Database").Value;
			}
		}

		#endregion Connection

		#region Twillio

		public static string TwilioFromNumber => config.GetSection("Twilio:FromNumber").Value;

		public static string TwilioAccountId => config.GetSection("Twilio:AccountId").Value;

		public static string TwilioAuthToken => config.GetSection("Twilio:AuthToken").Value;

		public static string TwilioBaseUrl => config.GetSection("Twilio:BaseUrl").Value;

		public static string RecipientMobileNo => config.GetSection("Twilio:RecipientMobileNo").Value;

		#endregion Twillio
	}
}