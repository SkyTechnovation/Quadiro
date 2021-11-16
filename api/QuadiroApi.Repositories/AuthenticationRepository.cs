using Dapper;
using QuadiroApi.Entities;
using QuadiroApi.Entities.Enums;
using QuadiroApi.Infrastructure;
using QuadiroApi.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuadiroApi.Repositories
{
	public class AuthenticationRepository : IAuthenticationRepository
	{
		private IDbConnection _dbConnection;

		public AuthenticationRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// This is a method for authentication. The use of this method is to check the entered details from user input against value exist in database for authentication.
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="userPassword"></param>
		/// <param name="userMobileNo"></param>
		/// <returns></returns>
		public AuthenticationResponseModel Authenticate(string userName, string userPassword, string userMobileNo)
		{
			try
			{
				using var conn = _dbConnection;
				var storedProcedure = "[dbo].[spUsers_Authenticate]";

				if (conn.State == ConnectionState.Closed)
					conn.Open();

				using var tran = conn.BeginTransaction();
				try
				{
					userPassword = EncryptionHelper.Encrypt(userPassword);
					var result = conn.QuerySingle<AuthenticationResponseModel>(storedProcedure,
						new { UserName = userName, UserPassword = userPassword },
						tran, null,
						commandType: CommandType.StoredProcedure);
					tran.Commit();

					if (result.StatusCode == 1)
					{
						var otpMessage = result.Otp.ToString() + " is your One Time Password (OTP).";
						new TwilioHelper().SendSms(userMobileNo, otpMessage);
					}
					return result;
				}
				catch (Exception ex)
				{
					tran.Rollback();
					return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.DatabaseError.ToString(), StatusMessage = ex.Message };
				};
			}
			catch (SqlException sqlEx)
			{
				return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.ConnectionError.ToString(), StatusMessage = sqlEx.Message };
			}
			catch (Exception ex)
			{
				return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.Exception.ToString(), StatusMessage = ex.Message };
			}
		}

		/// <summary>
		/// This is a method to do cross verification of the entered one time password against values in database.
		/// </summary>
		/// <param name="userOtpModel"></param>
		/// <returns></returns>
		public AuthenticationResponseModel OtpVerify(UserOtpModel userOtpModel)
		{
			try
			{
				using var conn = _dbConnection;
				var storedProcedure = "[dbo].[spUserOtp_Verify]";

				if (conn.State == ConnectionState.Closed)
					conn.Open();

				using var tran = conn.BeginTransaction();
				try
				{
					var result = conn.QuerySingle<AuthenticationResponseModel>(storedProcedure,
						new { UserID = userOtpModel.UserID, Otp = userOtpModel.Otp },
						tran, null,
						commandType: CommandType.StoredProcedure);
					tran.Commit();

					return result;
				}
				catch (Exception ex)
				{
					tran.Rollback();
					return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.DatabaseError.ToString(), StatusMessage = ex.Message };
				};
			}
			catch (SqlException sqlEx)
			{
				return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.ConnectionError.ToString(), StatusMessage = sqlEx.Message };
			}
			catch (Exception ex)
			{
				return new AuthenticationResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.Exception.ToString(), StatusMessage = ex.Message };
			}
		}
	}
}