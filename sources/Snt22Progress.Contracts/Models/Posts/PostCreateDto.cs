using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Posts
{
	public class PostCreateDto
	{
		[Required]
		public string Title { get; set; }

		[Required]
		public string Text { get; set; }
	}
}
