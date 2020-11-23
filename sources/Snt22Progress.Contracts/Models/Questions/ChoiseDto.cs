using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Questions
{
	public class ChoiseDto : IDto<int>
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public int VotesCount { get; set; }
	}
}
