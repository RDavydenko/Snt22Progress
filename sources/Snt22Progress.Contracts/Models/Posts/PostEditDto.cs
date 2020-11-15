using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Models.Posts
{
	public class PostEditDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }
	}
}
