using Newtonsoft.Json;
using System.Net;

namespace QuadiroApi.Infrastructure
{
	public class APIHelper
	{
		public static dynamic GetResponseFromApi(string apiUrl, string methodType, string queryString)
		{
			var myRequest = new WebRequestHelper(apiUrl, methodType, queryString);
			dynamic myResponse = JsonConvert.DeserializeObject(myRequest.GetResponse());
			return myResponse;
		}

		public static string GetStringResponseFromApi(string apiUrl, string methodType, string queryString)
		{
			var myRequest = new WebRequestHelper(apiUrl, methodType, queryString);
			return WebUtility.HtmlDecode(myRequest.GetResponse());
		}

		public static string GetStringResponseFromApi(string apiUrl, string methodType, string queryString, string authorization)
		{
			var myWebRequest = new WebRequestHelper(apiUrl, methodType, queryString, authorization);
			return WebUtility.HtmlDecode(myWebRequest.GetResponse());
		}
	}
}