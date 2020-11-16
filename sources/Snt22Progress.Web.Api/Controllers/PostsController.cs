using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

		/// <summary>
		/// Получить список постов
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<PostGetDto>>> GetPosts()
		{
			return await _postsService.GetPostsAsync();
		}

		/// <summary>
		/// Получить пост по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор поста</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ResultResponse<PostGetDto>> GetPosts([FromQuery] int id)
		{
			return await _postsService.GetPostAsync(id);
		}

		/// <summary>
		/// Добавить новый пост
		/// </summary>
		/// <param name="dto">Пост</param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("add")]
		public async Task<ResultResponse<PostGetDto>> AddPost([FromBody] PostCreateDto dto)
		{
			if (dto == null || !ModelState.IsValid)
			{
				return ResultResponse<PostGetDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _postsService.CreatePostAsync(dto, UserId.Value);
		}

		/// <summary>
		/// Изменить существующий пост
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <param name="dto">Пост</param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("{id}/edit")]
		public async Task<ResultResponse<PostGetDto>> EditPost([FromRoute] int id, [FromBody] PostEditDto dto)
		{
			if (dto == null || !ModelState.IsValid)
			{
				return ResultResponse<PostGetDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _postsService.UpdatePostAsync(id, dto, UserId.Value);
		}

		/// <summary>
		/// Удалить существующий пост
		/// </summary>
		/// <param name="id">Идентификатор поста</param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost("{id}/delete")]
		public async Task<ResultResponse> DeletePost([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _postsService.DeletePostAsync(id);
		}
	}
}
