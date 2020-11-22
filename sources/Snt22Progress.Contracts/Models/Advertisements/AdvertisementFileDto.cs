using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.Contracts.Models.Advertisements
{
	/// <summary>
	/// ДТО файла объявления о продаже участка
	/// </summary>
	public class AdvertisementFileDto : IDto<int>
	{
		public int Id { get; set; }

		public string NativeName { get; set; }

		public string Path { get; set; }

		public int Length { get; set; }

		public DateTime Uploaded { get; set; }

		public Uploader Uploader { get; set; }
	}
}
