using Newtonsoft.Json;
using System;

namespace QuadiroApi.Entities
{
	public class ProductModel
	{
		[JsonProperty("product_id")]
		public Guid? ProductID { get; set; }

		[JsonProperty("product_name")]
		public string ProductName { get; set; }

		[JsonProperty("product_quantity")]
		public decimal ProductQuantity { get; set; }

		[JsonProperty("product_price")]
		public decimal ProductPrice { get; set; }

		[JsonProperty("product_image")]
		public string ProductImageUrl { get; set; }

		public ProductModel()
		{
			ProductID = null;
			ProductName = ProductImageUrl = string.Empty;
			ProductQuantity = ProductPrice = 0;
		}
	}
}