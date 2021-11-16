using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuadiroApi.Infrastructure
{
	public class WebRequestHelper
	{
		private readonly WebRequest _request;
		private Stream _dataStream;

		public string Status { get; set; }

		public WebRequestHelper()
		{
		}

		public WebRequestHelper(string url)
		{
			_request = WebRequest.Create(url);
		}

		public WebRequestHelper(string url, string method) : this(url)
		{
			if (method.Equals("GET") || method.Equals("POST"))
				_request.Method = method;
			else
				throw new Exception("Invalid Method Type");
		}

		public WebRequestHelper(string url, string method, string data) : this(url, method)
		{
			var postData = data;
			var byteArray = Encoding.UTF8.GetBytes(postData);
			_request.ContentType = "application/x-www-form-urlencoded";
			_request.ContentLength = byteArray.Length;
			_request.Timeout = 6000000;
			_dataStream = _request.GetRequestStream();
			_dataStream.Write(byteArray, 0, byteArray.Length);
			_dataStream.Close();
		}

		public WebRequestHelper(string url, string method, string data, string authorization) : this(url, method)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			_request.ContentType = "application/x-www-form-urlencoded";
			_request.ContentLength = bytes.Length;
			_request.Timeout = 6000000;
			bool flag = !string.IsNullOrWhiteSpace(authorization.Trim());
			if (flag)
				_request.Headers.Add(HttpRequestHeader.Authorization, authorization);
			_dataStream = this._request.GetRequestStream();
			_dataStream.Write(bytes, 0, bytes.Length);
			_dataStream.Close();
		}

		public string GetResponse()
		{
			var response = _request.GetResponse();
			Status = ((HttpWebResponse)response).StatusDescription;
			_dataStream = response.GetResponseStream();
			var reader = new StreamReader(_dataStream);
			var responseFromServer = reader.ReadToEnd();
			reader.Close();
			_dataStream.Close();
			response.Close();

			return responseFromServer;
		}

		public async Task<string> Post(string baseUrl, string apiUrl, string jsonData)
		{
			try
			{
				var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
				var bytes = Encoding.UTF8.GetBytes(jsonData);
				var byteContent = new ByteArrayContent(bytes);
				byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var response = await httpClient.PostAsync(apiUrl, byteContent);
				var responseParse = await response.Content.ReadAsStringAsync();
				return responseParse;
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}