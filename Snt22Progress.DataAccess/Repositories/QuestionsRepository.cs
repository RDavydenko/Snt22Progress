using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class QuestionsRepository : BasePostgresRepository<Question>, IRepository<Question, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "questions";

		public QuestionsRepository(string connection) : base(connection) { }
	}
}
