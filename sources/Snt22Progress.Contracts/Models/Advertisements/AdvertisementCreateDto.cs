using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Snt22Progress.Contracts.Models.Advertisements
{
	/// <summary>
	/// ДТО для создания объявления о продаже участка
	/// </summary>
	public class AdvertisementCreateDto
	{
		public string Title { get; set; }

		public string Text { get; set; }

		public decimal Price { get; set; }

		public string Phone { get; set; }

		public IFormFile Image { get; set; }

		public bool IsPrivatizated { get; set; } = false;

		public int Square { get; set; }
	}
}
