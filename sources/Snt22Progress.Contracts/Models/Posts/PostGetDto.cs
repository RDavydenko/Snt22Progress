using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.Contracts.Models.Posts
{
	public class PostGetDto : IDto<int>
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public DateTime Created { get; set; }

		public Creator Creator { get; set; }

		public DateTime? Edited { get; set; }

		public Editor Editor { get; set; }
	}
}
