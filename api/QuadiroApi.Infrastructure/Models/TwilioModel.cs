namespace QuadiroApi.Infrastructure.Models
{
	public class TwilioSmsEntity
	{
		public string account_sid { get; set; }
		public string api_version { get; set; }
		public string body { get; set; }
		public string date_created { get; set; }
		public string date_sent { get; set; }
		public string date_updated { get; set; }
		public string direction { get; set; }
		public int? error_code { get; set; }
		public string error_message { get; set; }
		public string from { get; set; }
		public string messaging_service_sid { get; set; }
		public string num_media { get; set; }
		public short num_segments { get; set; }
		public double? price { get; set; }
		public string price_unit { get; set; }
		public string sid { get; set; }
		public string status { get; set; }
		public string to { get; set; }
		public string uri { get; set; }
		public int? code { get; set; }
		public string message { get; set; }
	}

	public class TwilioSmsResponseModel
	{
		public bool IsSuccessful { get; set; }
		public short? SmsGatewayID { get; set; }
		public string SmsID { get; set; }
		public string FromMobileNo { get; set; }
		public short SmsSegments { get; set; }
		public string SmsStatus { get; set; }
		public int? ErrorCode { get; set; }
		public string ErrorMessage { get; set; }

		public TwilioSmsResponseModel()
		{
			SmsID = string.Empty;
		}
	}
}