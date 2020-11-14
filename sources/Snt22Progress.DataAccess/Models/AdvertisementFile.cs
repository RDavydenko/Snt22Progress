using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class AdvertisementFile : IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public int Length { get; set; }		

		public DateTime Uploaded { get; set; }

		public int? Uploader_Id { get; set; }
	}
}
