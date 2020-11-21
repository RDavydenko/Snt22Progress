using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	/// <summary>
	/// Репозиторий для работы с файлами о должниках
	/// </summary>
	public class DebtorFilesRepository : BasePostgresRepository<DebtorFile>, IRepository<DebtorFile, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "debtorfiles";

		public DebtorFilesRepository(string connection) : base(connection) { }
	}
}
