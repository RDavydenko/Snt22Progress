using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Snt22Progress.DataAccess.Infrastructure
{
	public abstract class BasePostgresRepository<TEntity, TId> : BaseSqlRepository<TEntity, TId>
		where TEntity : class, IBaseEntity<TId>, new()
	{
		public BasePostgresRepository(string connection) : base(connection)
		{
		}

		protected override IDbConnection GetNewConnection()
		{
			return new NpgsqlConnection(_connection);
		}
	}
}
