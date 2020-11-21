using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Legislations
{
	/// <summary>
	/// ДТО законодательства
	/// </summary>
	public class LegislationDto : IDto<int>
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public string Url { get; set; }

		public DateTime Created { get; set; }

		public int? CreatorId { get; set; }
	}
}
