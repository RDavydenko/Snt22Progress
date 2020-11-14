using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.DataAccess.Infrastructure
{
	/// <summary>
	/// Список типов SQL-строк
	/// </summary>
	public enum SqlStringType
	{
		Select = 0,
		Insert = 1,
		Update = 2,
		Delete = 3
	}
}
