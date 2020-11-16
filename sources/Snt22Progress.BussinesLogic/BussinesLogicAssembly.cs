using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Snt22Progress.BussinesLogic
{
	public static class BussinesLogicAssembly
	{
		public static Assembly Assembly { get; set; }

		static BussinesLogicAssembly()
		{
			Assembly = typeof(BussinesLogicAssembly).Assembly;
		}
	}
}
