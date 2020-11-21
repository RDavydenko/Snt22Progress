using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class LegislationsRepository : BasePostgresRepository<Legislation>, IRepository<Legislation, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "legislations";

		public LegislationsRepository(string connection) : base (connection) { }
	}
}
