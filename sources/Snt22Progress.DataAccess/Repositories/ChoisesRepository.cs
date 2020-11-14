using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class ChoisesRepository : BasePostgresRepository<Choise>, IRepository<Choise, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "choises";

		public ChoisesRepository(string connection) : base(connection)
		{

		}
	}
}
