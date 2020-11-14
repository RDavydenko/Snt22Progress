using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class PostsRepository : BasePostgresRepository<Post>, IRepository<Post, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "posts";

		public PostsRepository(string connection) : base(connection)
		{

		}
	}
}
