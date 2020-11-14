using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class UserToRolesRepository : BasePostgresRepository<UserToRole>, IRepository<UserToRole, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "userstoroles";

		public UserToRolesRepository(string connection) : base(connection)
		{

		}
	}
}
