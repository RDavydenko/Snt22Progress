using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.DebtorFiles
{
	public class DebtorFileDto : IDto<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string NativeName { get; set; }

		public string Path { get; set; }

		public int Length { get; set; }

		public DateTime Uploaded { get; set; }

		public int? UploaderId { get; set; }
	}
}
