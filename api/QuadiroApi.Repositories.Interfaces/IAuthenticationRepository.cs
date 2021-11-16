using QuadiroApi.Entities;

namespace QuadiroApi.Repositories.Interfaces
{
	public interface IAuthenticationRepository
	{
		/// <summary>
		/// This is an interface for repository
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="userPassword"></param>
		/// <param name="userMobileNo"></param>
		/// <returns></returns>
		AuthenticationResponseModel Authenticate(string userName, string userPassword, string userMobileNo);

		/// <summary>
		/// This is an interface for repository
		/// </summary>
		/// <param name="userOtpModel"></param>
		/// <returns></returns>
		AuthenticationResponseModel OtpVerify(UserOtpModel userOtpModel);
	}
}