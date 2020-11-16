using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class DocumentViewsRepository : BasePostgresViewRepository<DocumentView>, IViewRepository<DocumentView, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "documentsview";

		public DocumentViewsRepository(string connection) : base(connection)
		{

		}
	}
}
