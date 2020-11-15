using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Posts
{
	public class PostCreateDto
	{
		public string Title { get; set; }

		public string Text { get; set; }
	}
}
