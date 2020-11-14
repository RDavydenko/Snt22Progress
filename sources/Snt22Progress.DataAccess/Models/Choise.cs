using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Choise : IBaseEntity<int>
	{
		public int Id { get; set; }

		public int Question_Id { get; set; }

		public string Text { get; set; }

		public int Votes_Count { get; set; } = 0;
	}
}
