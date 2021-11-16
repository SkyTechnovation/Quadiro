using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuadiroApi.Entities;
using QuadiroApi.Infrastructure;
using QuadiroApi.Repositories.Interfaces;
using System;

namespace QuadiroApi.Controllers
{
	/// <summary>
	/// This is a controller for General Setting. The use of general setting is to get pre-defined mobile number from config file.
	/// </summary>
	[ApiController, Route("generalsetting")]
	[ApiExplorerSettings(GroupName = "General Setting")]
	public class GeneralSettingController : Controller
	{
		private readonly IGeneralSettingRepository _iGeneralSettingRepository;

		/// <summary>
		/// Here is a constructor being used for controller of General Setting
		/// </summary>
		/// <param name="iGeneralSettingRepository"></param>
		public GeneralSettingController(IGeneralSettingRepository iGeneralSettingRepository)
		{
			_iGeneralSettingRepository = iGeneralSettingRepository;
		}

		/// <summary>
		/// This API is used to fetch pre-exist mobile number from config file.
		/// </summary>
		/// <returns></returns>
		[HttpGet("mobileno")]
		public ActionResult GetDefaultMobileNo()
		{
			try
			{
				var result = _iGeneralSettingRepository.GetDefaultMobileNo();
				var message = result.StatusCode.ToInt() == 1 ? "Get Default Mobile No." : result.StatusMessage;

				return new JsonResult(new ResponseModel { StatusCode = result.StatusCode.ToInt(), StatusType = string.Empty, StatusMessage = message, Data = result.Data }) { StatusCode = StatusCodes.Status200OK };
			}
			catch (Exception exception)
			{
				return new JsonResult(new ExceptionResponseModel(exception.Message)) { StatusCode = StatusCodes.Status403Forbidden };
			}
		}
	}
}