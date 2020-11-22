using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	/// <summary>
	/// Репоризорий представления (view) для объявлений о продаже участка
	/// </summary>
	public class AdvertisementViewsRepository : BasePostgresViewRepository<AdvertisementView>, IViewRepository<AdvertisementView, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "advertisementsview";

		public AdvertisementViewsRepository(string connection) : base(connection) { }
	}
}
