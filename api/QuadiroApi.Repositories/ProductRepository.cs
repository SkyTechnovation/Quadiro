using Dapper;
using QuadiroApi.Entities;
using QuadiroApi.Entities.Enums;
using QuadiroApi.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuadiroApi.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private IDbConnection _dbConnection;

		public ProductRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		/// <summary>
		/// This method is to get a product listing from database. We are also fetching product name, price, image and also quantity from database.
		/// </summary>
		/// <returns></returns>
		public ResponseModel Get()
		{
			try
			{
				using var conn = _dbConnection;
				var storedProcedure = "[dbo].[spProducts_Get]";

				if (conn.State == ConnectionState.Closed)
					conn.Open();

				using var tran = conn.BeginTransaction();
				try
				{
					var result = conn.Query<ProductModel>(storedProcedure, new { }, tran, commandType: CommandType.StoredProcedure);
					tran.Commit();

					return new ResponseModel() { StatusCode = 3, StatusType = "get", StatusMessage = "success", Data = result };
				}
				catch (Exception ex)
				{
					tran.Rollback();
					return new ResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.DatabaseError.ToString(), StatusMessage = ex.Message };
				};
			}
			catch (SqlException sqlEx)
			{
				return new ResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.ConnectionError.ToString(), StatusMessage = sqlEx.Message };
			}
			catch (Exception ex)
			{
				return new ResponseModel() { StatusCode = 500, StatusType = StatusTypeEnum.Exception.ToString(), StatusMessage = ex.Message };
			}
		}
	}
}