using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[Route("api/posts")]
	public class PostsController : BaseApiController
	{
		private readonly IRepository<Post, int> _postsRepository;

		public PostsController(IRepository<Post, int> postsRepository)
		{
			_postsRepository = postsRepository;
		}

	}
}
