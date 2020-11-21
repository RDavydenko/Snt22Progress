using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Models.Legislations
{
	/// <summary>
	/// ДТО для создания законодательства
	/// </summary>
	public class LegislationCreateDto
	{
		public string Text { get; set; }

		public string Url { get; set; }
	}
}
