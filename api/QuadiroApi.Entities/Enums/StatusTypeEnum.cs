namespace QuadiroApi.Entities.Enums
{
	public enum StatusTypeEnum
	{
		DatabaseError,
		ConnectionError,
		Exception
	}

	public enum StatusCodeEnum
	{
		create,
		update,
		remove,
		duplicate
	}
}