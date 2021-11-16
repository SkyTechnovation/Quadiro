using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuadiroApi.Entities;
using QuadiroApi.Entities.Enums;
using QuadiroApi.Infrastructure;
using QuadiroApi.Repositories.Interfaces;
using System;
using System.Data.SqlClient;

namespace QuadiroApi.Controllers
{
	/// <summary>
	/// This is a controller for Login as well as one time password verification
	/// </summary>
	[ApiController, Route("authentication")]
	[ApiExplorerSettings(GroupName = "Authentication API")]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationRepository _iAuthenticationRepository;

		/// <summary>
		/// Here is a constuctor for above controller.
		/// </summary>
		/// <param name="iAuthenticationRepository"></param>
		public AuthenticationController(IAuthenticationRepository iAuthenticationRepository)
		{
			_iAuthenticationRepository = iAuthenticationRepository;
		}

		/// <summary>
		/// Authentication API used to send a one time password on the entered mobile number from Login screen.
		/// </summary>
		/// <param name="authenticationModel"></param>
		/// <returns></returns>
		[HttpPost("")]
		public ActionResult Authenticate([FromBody] AuthenticationModel authenticationModel)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				if (authenticationModel == null)
					return BadRequest();

				if (string.IsNullOrEmpty(authenticationModel.UserName))
					return new JsonResult(new ResponseModel { StatusCode = 400, Data = new ErrorModel { StatusCode = 5, Name = ErrorNameEnum.ParameterValueError.ToString(), Message = "Please enter username" } }) { StatusCode = StatusCodes.Status200OK };
				if (string.IsNullOrEmpty(authenticationModel.UserPassword))
					return new JsonResult(new ResponseModel { StatusCode = 400, Data = new ErrorModel { StatusCode = 5, Name = ErrorNameEnum.ParameterValueError.ToString(), Message = "Please enter valid password" } }) { StatusCode = StatusCodes.Status200OK };
				if (string.IsNullOrEmpty(authenticationModel.UserMobileNo))
					return new JsonResult(new ResponseModel { StatusCode = 400, Data = new ErrorModel { StatusCode = 5, Name = ErrorNameEnum.ParameterValueError.ToString(), Message = "Please enter mobile number" } }) { StatusCode = StatusCodes.Status200OK };

				var result = _iAuthenticationRepository.Authenticate(authenticationModel.UserName.ToLower(), authenticationModel.UserPassword, authenticationModel.UserMobileNo);
				var type = (result.StatusCode.ToInt() == 1101 || result.StatusCode.ToInt() == 1102) ? StatusCodeEnum.duplicate.ToString() : string.Empty;
				var message = result.StatusCode.ToInt() == 1 ? "Authentication Successfully & OTP sent on entered mobile number" : result.StatusCode.ToInt() == 1101 ? "Please enter correct user name." : result.StatusCode.ToInt() == 1102 ? "Please enter correct user password." : result.StatusMessage;
				var statusCode = (result.StatusCode.ToInt() == 1101 || result.StatusCode.ToInt() == 1102) ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;

				var data = new { user_id = result.UserID, user_name = authenticationModel.UserName };
				return new JsonResult(new ResponseModel { StatusCode = result.StatusCode.ToInt(), StatusType = type, StatusMessage = message, Data = data }) { StatusCode = statusCode };
			}
			catch (SqlException exception)
			{
				return new JsonResult(new SqlExceptionResponseModel(exception.Message)) { StatusCode = StatusCodes.Status403Forbidden };
			}
			catch (Exception exception)
			{
				return new JsonResult(new ExceptionResponseModel(exception.Message)) { StatusCode = StatusCodes.Status403Forbidden };
			}
		}

		/// <summary>
		/// This API will be used to verify an OTP entered from frontend side. We will check entered OTP against our value in database and we will allow user to login if it matches or we will fire validation message if OTP is wrong.
		/// </summary>
		/// <param name="userOtpModel"></param>
		/// <returns></returns>
		[HttpPost("otp")]
		public ActionResult OtpVerify([FromBody] UserOtpModel userOtpModel)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);
				if (userOtpModel == null)
					return BadRequest();

				if (userOtpModel.UserID == null || userOtpModel.UserID == Guid.Empty)
					return new JsonResult(new ResponseModel { StatusCode = 400, Data = new ErrorModel { StatusCode = 5, Name = ErrorNameEnum.ParameterValueError.ToString(), Message = "User Id is required" } }) { StatusCode = StatusCodes.Status200OK };
				if (string.IsNullOrEmpty(userOtpModel.Otp))
					return new JsonResult(new ResponseModel { StatusCode = 400, Data = new ErrorModel { StatusCode = 5, Name = ErrorNameEnum.ParameterValueError.ToString(), Message = "Otp is required" } }) { StatusCode = StatusCodes.Status200OK };

				var result = _iAuthenticationRepository.OtpVerify(userOtpModel);
				var type = (result.StatusCode.ToInt() == 1101 || result.StatusCode.ToInt() == 1102 || result.StatusCode.ToInt() == 1103) ? StatusCodeEnum.duplicate.ToString() : string.Empty;
				var message = result.StatusCode.ToInt() == 1 ? "OTP verification is successful" : result.StatusCode.ToInt() == 1101 ? "Entered OTP is incorrect, Please try again." : result.StatusCode.ToInt() == 1102 ? "This OTP is already verified" : result.StatusCode.ToInt() == 1103 ? "Entered OTP is expired" : result.StatusMessage;
				var statusCode = (result.StatusCode.ToInt() == 1101 || result.StatusCode.ToInt() == 1102 || result.StatusCode.ToInt() == 1103) ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;

				return new JsonResult(new ResponseModel { StatusCode = result.StatusCode.ToInt(), StatusType = type, StatusMessage = message, Data = null }) { StatusCode = statusCode };
			}
			catch (SqlException exception)
			{
				return new JsonResult(new SqlExceptionResponseModel(exception.Message)) { StatusCode = StatusCodes.Status403Forbidden };
			}
			catch (Exception exception)
			{
				return new JsonResult(new ExceptionResponseModel(exception.Message)) { StatusCode = StatusCodes.Status403Forbidden };
			}
		}
	}
}