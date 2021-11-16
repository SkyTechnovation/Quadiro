using Newtonsoft.Json;
using QuadiroApi.Entities.Enums;

namespace QuadiroApi.Entities
{
	public class ResponseModel
	{
		[JsonProperty("code")]
		public int StatusCode { get; set; }

		[JsonProperty("type")]
		public string StatusType { get; set; }

		[JsonProperty("message")]
		public string StatusMessage { get; set; }

		[JsonProperty("data")]
		public dynamic Data { get; set; }

		public ResponseModel()
		{
			StatusType = StatusMessage = string.Empty;
		}
	}

	public class ErrorModel
	{
		[JsonProperty("code")]
		public int StatusCode { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }
	}

	public class ErrorResponseModel
	{
		[JsonProperty("error")]
		public ErrorModel Error { get; set; }
	}

	public class ExceptionResponseModel
	{
		[JsonProperty("error")]
		public ErrorModel Error { get; set; }

		public ExceptionResponseModel(string exceptionMessage)
		{
			Error = new ErrorModel { StatusCode = 500, Name = ErrorNameEnum.Exception.ToString(), Message = exceptionMessage };
		}
	}

	public class SqlExceptionResponseModel
	{
		[JsonProperty("error")]
		public ErrorModel Error { get; set; }

		public SqlExceptionResponseModel(string exceptionMessage)
		{
			Error = new ErrorModel { StatusCode = 500, Name = ErrorNameEnum.DatabaseError.ToString(), Message = exceptionMessage };
		}
	}
}