using Newtonsoft.Json;
using QuadiroApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace QuadiroApi.Infrastructure
{
	public class TwilioHelper
	{
		private readonly string _fromNumber, _twilioAccountId, _twilioAuthToken, _twilioBaseUrl = string.Empty;

		public TwilioHelper()
		{
			_fromNumber = ConfigHelper.TwilioFromNumber;
			_twilioAccountId = ConfigHelper.TwilioAccountId;
			_twilioAuthToken = ConfigHelper.TwilioAuthToken;
			_twilioBaseUrl = string.Format(ConfigHelper.TwilioBaseUrl, _twilioAccountId);
		}

		/// <summary>
		/// This API is used to send text message request to twilio for one time password.
		/// </summary>
		/// <param name="recipientMobileNo"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public TwilioSmsResponseModel SendSms(string recipientMobileNo, string content)
		{
			try
			{
				var plainTextBytes = Encoding.UTF8.GetBytes(_twilioAccountId + ":" + _twilioAuthToken);
				var authorizationToken = Convert.ToBase64String(plainTextBytes);
				var smsResponseEntity = new TwilioSmsResponseModel();

				var dictionary = new Dictionary<string, string>
								{
										{ "From", _fromNumber },
										{ "To", recipientMobileNo },
										{ "Body", content },
										{ "StatusCallback" , "https://pakapi.captok.com/omni/twilio/webhook"}
								};

				var reponsejSon = Post(_twilioBaseUrl + "Messages.json", authorizationToken, dictionary);
				var twilioSmsEntity = JsonConvert.DeserializeObject<TwilioSmsEntity>(reponsejSon);

				smsResponseEntity.FromMobileNo = twilioSmsEntity.from;
				smsResponseEntity.SmsStatus = twilioSmsEntity.status;
				smsResponseEntity.SmsID = twilioSmsEntity.sid;
				smsResponseEntity.SmsSegments = twilioSmsEntity.num_segments;
				smsResponseEntity.ErrorCode = twilioSmsEntity.error_code;
				smsResponseEntity.IsSuccessful = smsResponseEntity.SmsStatus == "queued";
				if (smsResponseEntity.IsSuccessful == false)
				{
					smsResponseEntity.ErrorCode = twilioSmsEntity.code;
					smsResponseEntity.ErrorMessage = twilioSmsEntity.message;
				}

				return smsResponseEntity;
			}
			catch (Exception ex)
			{
				return new TwilioSmsResponseModel { IsSuccessful = false, ErrorMessage = ex.Message };
			}
		}

		private string Post(string url, string token, Dictionary<string, string> postParameters = null)
		{
			var result = string.Empty;

			try
			{
				var postData = string.Empty;
				if (postParameters?.Count > 0)
				{
					if (postParameters.Keys?.Count > 0)
					{
						foreach (string key in postParameters.Keys)
						{
							postData += HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(postParameters[key]) + "&";
						}
					}
				}
				var data = Encoding.ASCII.GetBytes(postData);

				var myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				myHttpWebRequest.Method = "POST";
				myHttpWebRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + token;
				myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
				myHttpWebRequest.ContentLength = data.Length;

				var requestStream = myHttpWebRequest.GetRequestStream();
				requestStream.Write(data, 0, data.Length);
				requestStream.Close();

				var myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
				var responseStream = myHttpWebResponse.GetResponseStream();
				var myStreamReader = new StreamReader(responseStream, Encoding.Default);
				var response = myStreamReader.ReadToEnd();

				myStreamReader.Close();
				responseStream.Close();
				myHttpWebResponse.Close();

				return response;
			}
			catch (WebException webEx)
			{
				if (!(webEx.Response is HttpWebResponse response)) return string.Empty;

				var exResponseStream = response.GetResponseStream();
				if (exResponseStream == null) { throw; }
				var stream = new StreamReader(exResponseStream, Encoding.UTF8);
				return stream.ReadToEnd();
			}
			catch
			{
				return null;
			}
		}
	}
}