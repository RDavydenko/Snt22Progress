using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Models.Advertisements
{
	/// <summary>
	/// ДТО для редактирования объявления о продаже участка
	/// </summary>
	public class AdvertisementEditDto
	{
		public string Title { get; set; }

		public string Text { get; set; }

		public decimal Price { get; set; }

		public bool IsPrivatizated { get; set; }

		public int Square { get; set; }
	}
}
