using Newtonsoft.Json;
using System;

namespace QuadiroApi.Entities
{
	public class AuthenticationModel
	{
		[JsonProperty("user_name")]
		public string UserName { get; set; }

		[JsonProperty("user_password")]
		public string UserPassword { get; set; }

		[JsonProperty("user_mobileno")]
		public string UserMobileNo { get; set; }

		public AuthenticationModel()
		{
			UserName = UserPassword = UserMobileNo = string.Empty;
		}
	}

	public class AuthenticationResponseModel : ResponseModel
	{
		[JsonProperty("user_id")]
		public Guid UserID { get; set; }

		[JsonProperty("otp")]
		public string Otp { get; set; }
	}

	public class UserOtpModel
	{
		[JsonProperty("user_id")]
		public Guid? UserID { get; set; }

		[JsonProperty("otp")]
		public string Otp { get; set; }
	}
}