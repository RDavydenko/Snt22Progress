using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories
{
	/// <summary>
	/// Репозиторий представления (view) для опросов
	/// </summary>
	public class QuestionViewsRepository : BasePostgresViewRepository<QuestionView>, IViewRepository<QuestionView, int>
	{
		public override string SchemaName => "progress";

		public override string TableName => "questionsview";
	
		public QuestionViewsRepository(string connection) : base(connection) { }
	}
}
