using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Npgsql;

namespace Snt22Progress.DataAccess.Infrastructure
{
	public abstract class BasePostgresViewRepository<TEntity> : BaseSqlViewRepository<TEntity>
		where TEntity : class, IBaseEntity<int>, new()
	{
		public BasePostgresViewRepository(string connection) : base(connection)
		{
		}

		protected override IDbConnection GetNewConnection()
		{
			var connection = new NpgsqlConnection(_connection);
			return connection;
		}
	}
}
