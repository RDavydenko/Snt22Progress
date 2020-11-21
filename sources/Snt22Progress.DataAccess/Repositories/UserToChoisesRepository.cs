using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class UserToChoisesRepository : BasePostgresRepository<UserToChoise>, IRepository<UserToChoise, int>
	{
		public override string SchemaName => "progress"; 

		public override string TableName => "usertochoises"; 

		public UserToChoisesRepository(string connection) : base(connection) { }
	}
}
