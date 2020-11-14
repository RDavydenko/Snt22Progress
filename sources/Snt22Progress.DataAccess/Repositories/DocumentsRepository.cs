using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class DocumentsRepository : BasePostgresRepository<Document>, IRepository<Document, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "documents";

		public DocumentsRepository(string connection) : base(connection)
		{

		}
	}
}
