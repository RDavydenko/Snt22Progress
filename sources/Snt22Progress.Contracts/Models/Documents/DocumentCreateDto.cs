using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Snt22Progress.Contracts.Models.Documents
{
	public class DocumentCreateDto
	{
		[Required]
		public IFormFile File { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
