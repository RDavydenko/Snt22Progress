using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class AdvertisementView : IBaseEntity<int>
	{
		public int id { get; set; }

		public string title { get; set; }

		public string text { get; set; }

		public decimal price { get; set; }

		public string phone { get; set; }

		public int? image_file_id { get; set; }
		public string image_path { get; set; }
		public int? image_length { get; set; }
		public string image_native_name { get; set; }
		public DateTime? image_uploaded { get; set; }
		public int? image_uploader_id { get; set; }
		public string image_uploader_fname { get; set; }
		public string image_uploader_lname { get; set; }
		public string image_uploader_mname { get; set; }

		public bool is_privatizated { get; set; }

		public int square { get; set; }

		public DateTime created { get; set; }
		public int? creator_id { get; set; }
		public string creator_fname { get; set; }
		public string creator_lname { get; set; }
		public string creator_mname { get; set; }

		public bool is_active { get; set; }
	}
}
