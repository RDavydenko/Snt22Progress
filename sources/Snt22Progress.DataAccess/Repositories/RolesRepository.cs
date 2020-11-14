using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class RolesRepository : BasePostgresRepository<Role>, IRepository<Role, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "roles";

		public RolesRepository(string connection) : base(connection)
		{

		}
	}
}
