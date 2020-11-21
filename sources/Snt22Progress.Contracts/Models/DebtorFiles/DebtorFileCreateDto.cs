using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Snt22Progress.Contracts.Models.DebtorFiles
{
	/// <summary>
	/// ДТО для добавления файла о должниках
	/// </summary>
	public class DebtorFileCreateDto
	{
		public string Name { get; set; }

		public IFormFile File { get; set; }
	}
}
