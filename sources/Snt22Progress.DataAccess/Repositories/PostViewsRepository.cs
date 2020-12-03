using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories.Interfaces;

namespace Snt22Progress.DataAccess.Repositories
{
	public class PostViewsRepository : BasePostgresViewRepository<PostView>, IPostViewRepository
	{
		public override string SchemaName => "progress";

		public override string TableName => "postsview";

		public PostViewsRepository(string connection) : base(connection)
		{

		}

		public async Task<int> GetCountAsync()
		{
			var sql = @$"SELECT COUNT(*)
						 FROM {SchemaName}.{TableName};";

			using (var connection = GetNewConnection())
			{
				connection.Open();

				var count = await connection.ExecuteScalarAsync<int>(sql);
				connection.Close();
				return count;
			}
		}
	}
}
