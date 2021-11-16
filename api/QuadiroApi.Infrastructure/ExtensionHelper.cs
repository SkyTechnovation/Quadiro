using System;

namespace QuadiroApi.Infrastructure
{
	public static class ExtensionHelper
	{
		public static int ToInt(this object obj)
		{
			try
			{
				return Convert.ToInt32(obj);
			}
			catch (Exception)
			{ return 0; }
		}
	}
}