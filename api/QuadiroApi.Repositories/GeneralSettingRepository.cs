using QuadiroApi.Entities;
using QuadiroApi.Entities.Enums;
using QuadiroApi.Infrastructure;
using QuadiroApi.Repositories.Interfaces;
using System;

namespace QuadiroApi.Repositories
{
	public class GeneralSettingRepository : IGeneralSettingRepository
	{
		/// <summary>
		/// This method will be used to get pre defined mobile number from config file.
		/// </summary>
		/// <returns></returns>
		public ResponseModel GetDefaultMobileNo()
		{
			try
			{
				try
				{
					return new ResponseModel() { StatusCode = 1, StatusType = "get", StatusMessage = "success", Data = ConfigHelper.RecipientMobileNo };
				}
				catch (Exception ex)
				{
					return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.DatabaseError.ToString(), StatusMessage = ex.Message };
				};
			}
			catch (Exception ex)
			{
				return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.Exception.ToString(), StatusMessage = ex.Message };
			}
		}
	}
}