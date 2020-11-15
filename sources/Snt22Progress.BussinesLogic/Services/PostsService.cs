using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Posts;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class PostsService : IPostsService
	{
		private readonly IRepository<Post, int> _postsRepository;
		private readonly IRepository<User, int> _usersRepository;
		private readonly IProgressLogger _progressLogger;
		private readonly IMapper _mapper;

		public PostsService(IRepository<Post, int> postsRepository,
			IRepository<User, int> usersRepository,
			IProgressLogger progressLogger,
			IMapper mapper)
		{
			_postsRepository = postsRepository;
			_usersRepository = usersRepository;
			_progressLogger = progressLogger;
			_mapper = mapper;
		}

		public async Task<ResultResponse<IEnumerable<PostGetDto>>> GetPostsAsync()
		{
			try
			{
				var posts = await _postsRepository.GetAsync();
				var postDtos = _mapper.Map<IEnumerable<PostGetDto>>(posts);
				return ResultResponse<IEnumerable<PostGetDto>>.GetSuccessResponse(postDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetPostsAsync));
				return ResultResponse<IEnumerable<PostGetDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<PostGetDto>> GetPostAsync(int id)
		{
			try
			{
				var post = await _postsRepository.GetAsync(id);
				if (post == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound);
				}
				var postDto = _mapper.Map<PostGetDto>(post);
				return ResultResponse <PostGetDto >.GetInternalServerErrorResponse();
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, id, GetType().Name, nameof(GetPostAsync));
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
		}		

		public async Task<ResultResponse<PostGetDto>> CreatePostAsync(PostCreateDto dto, int creatorId)
		{
			try
			{
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, creatorId }, GetType().Name, nameof(CreatePostAsync));
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<PostGetDto>> UpdatePostAsync(PostEditDto dto, int editorId)
		{
			try
			{
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, editorId }, GetType().Name, nameof(UpdatePostAsync));
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
		}
		public async Task<ResultResponse> DeletePostAsync(int id)
		{
			try
			{
				return ResultResponse.GetInternalServerErrorResponse();
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, id, GetType().Name, nameof(DeletePostAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
