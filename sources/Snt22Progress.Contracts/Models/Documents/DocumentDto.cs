using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.Contracts.Models.Documents
{
	public class DocumentDto : IDto<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string NativeName { get; set; }

		public string Path { get; set; }

		public int Length { get; set; }

		public Creator Creator { get; set; }

		public Editor Editor { get; set; }

		public bool IsActive { get; set; }
	}
}
