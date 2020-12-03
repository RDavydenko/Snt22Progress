using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.DataAccess.Repositories.Interfaces
{
	public interface IPostViewRepository : IViewRepository<PostView, int>
	{
		Task<int> GetCountAsync();
	}
}
