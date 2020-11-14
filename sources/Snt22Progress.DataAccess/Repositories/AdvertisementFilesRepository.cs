using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class AdvertisementFilesRepository : BasePostgresRepository<AdvertisementFile>, IRepository<AdvertisementFile, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "advertisementfiles";

		public AdvertisementFilesRepository(string connection) : base(connection)
		{

		}
	}
}
