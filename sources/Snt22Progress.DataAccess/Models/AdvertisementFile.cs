using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class AdvertisementFile : IBaseEntity<int>
	{
		public int id { get; set; }

		public string name { get; set; }

		public string native_name { get; set; }

		public string path { get; set; }

		public int length { get; set; }		

		public DateTime uploaded { get; set; }

		public int? Uploader_Id { get; set; }
	}
}
