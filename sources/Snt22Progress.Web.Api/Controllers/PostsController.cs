using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Posts;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[Route("api/posts")]
	public class PostsController : BaseApiController
	{
		private readonly IPostsService _postsService;

		public PostsController(IPostsService postsService)
		{
			_postsService = postsService;
		}

		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<PostGetDto>>> GetPosts()
		{
			return await _postsService.GetPostsAsync();
		}

		/// <summary>
		/// Добавить новый пост
		/// </summary>
		/// <param name="dto">Пост</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<PostGetDto>> AddPost(PostCreateDto dto)
		{
			if (dto != null && ModelState.IsValid)
			{
				return ResultResponse<PostGetDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _postsService.CreatePostAsync(dto, UserId.Value);
		}
	}
}
