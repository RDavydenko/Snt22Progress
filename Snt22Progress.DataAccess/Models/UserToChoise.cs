using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class UserToChoise : IBaseEntity<int>
	{
		public int Id { get; set; }
		
		public int User_Id { get; set; }

		public int Choise_Id { get; set; }
	}
}
