using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class ValuePairsRepository : BasePostgresRepository<ValuePair>, IRepository<ValuePair, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "valuepairs";

		public ValuePairsRepository(string connection) : base(connection)
		{

		}
	}
}
