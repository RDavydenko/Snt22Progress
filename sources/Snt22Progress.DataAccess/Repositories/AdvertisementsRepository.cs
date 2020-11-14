using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	public class AdvertisementsRepository : BasePostgresRepository<Advertisement>, IRepository<Advertisement, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "advertisements";

		public AdvertisementsRepository(string connection) : base(connection)
		{

		}
	}
}
