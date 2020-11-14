using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Snt22Progress.DataAccess.Infrastructure
{
	public abstract class BasePostgresRepository<TEntity> : BaseSqlRepository<TEntity>
		where TEntity : class, IBaseEntity<int>, new()
	{
		public BasePostgresRepository(string connection) : base(connection)
		{
		}

		protected override IDbConnection GetNewConnection()
		{
			var connection =  new NpgsqlConnection(_connection);
			return connection;
		}
	}
}
