using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class PostViewsRepository : BasePostgresViewRepository<PostView>, IViewRepository<PostView, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "postsview";

		public PostViewsRepository(string connection) : base(connection)
		{

		}
	}
}
