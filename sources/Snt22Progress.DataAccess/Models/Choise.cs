using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Choise : IBaseEntity<int>
	{
		public int id { get; set; }

		public int question_id { get; set; }

		public string text { get; set; }

		public int votes_count { get; set; } = 0;
	}
}
