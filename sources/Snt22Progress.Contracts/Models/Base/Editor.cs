﻿using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Base
{
	public class Editor : IDto<int>
	{
		public int Id { get; set; }

		public string FName { get; set; }

		public string LName { get; set; }

		public string MName { get; set; }
	}
}
