using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.BussinesLogic.Mappers
{
	public static class MapperHelper
	{
		public static Creator InitCreator(int? id, string fname, string lname, string mname)
		{
			if (id.HasValue)
			{
				return new Creator { Id = id.Value, FName = fname, LName = lname, MName = mname };
			}
			else
			{
				return null;
			}
		}

		public static Editor InitEditor(int? id, string fname, string lname, string mname)
		{
			if (id.HasValue)
			{
				return new Editor { Id = id.Value, FName = fname, LName = lname, MName = mname };
			}
			else
			{
				return null;
			}
		}

		public static Uploader InitUploader(int? id, string fname, string lname, string mname)
		{
			if (id.HasValue)
			{
				return new Uploader { Id = id.Value, FName = fname, LName = lname, MName = mname };
			}
			else
			{
				return null;
			}
		}
	}
}
