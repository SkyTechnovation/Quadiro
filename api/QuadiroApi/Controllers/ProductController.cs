using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuadiroApi.Entities;
using QuadiroApi.Repositories.Interfaces;
using System;
using System.Data.SqlClient;

namespace QuadiroApi.Controllers
{
	/// <summary>
	/// This is a controller to get product listing
	/// </summary>
	[ApiController, Route("product")]
	[ApiExplorerSettings(GroupName = "Product Listing")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _iProductRepository;

		/// <summary>
		/// This is a constructor method used in Product Controller
		/// </summary>
		/// <param name="iProductRepository"></param>
		public ProductController(IProductRepository iProductRepository)
		{
			_iProductRepository = iProductRepository;
		}

		/// <summary>
		/// Product API is used to get information of a products including product name, image, prices, as well as quantity.
		/// </summary>
		/// <returns></returns>
		[HttpGet("")]
		public ActionResult Get()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var result = _iProductRepository.Get();
				return new JsonResult(result);
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