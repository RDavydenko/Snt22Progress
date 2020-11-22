using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.Contracts.Models.Advertisements
{
	/// <summary>
	/// ДТО объявления о продаже участка
	/// </summary>
	public class AdvertisementDto : IDto<int>
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public decimal Price { get; set; }

		public AdvertisementFileDto Image { get; set; }

		public bool IsPrivatizated { get; set; } = false;

		public int Square { get; set; }

		public DateTime Created { get; set; }

		public Creator Creator { get; set; }
	}
}
