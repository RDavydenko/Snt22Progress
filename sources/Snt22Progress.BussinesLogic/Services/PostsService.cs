using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Posts;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories.Interfaces;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class PostsService : IPostsService
	{
		private readonly IRepository<Post, int> _postsRepository;
		private readonly IPostViewRepository _postsViewRepository;
		private readonly IRepository<User, int> _usersRepository;
		private readonly IProgressLogger _progressLogger;
		private readonly IMapper _mapper;

		public PostsService(IRepository<Post, int> postsRepository,
			IPostViewRepository postsViewRepository,
			IRepository<User, int> usersRepository,
			IProgressLogger progressLogger,
			IMapper mapper)
		{
			_postsRepository = postsRepository;
			_postsViewRepository = postsViewRepository;
			_usersRepository = usersRepository;
			_progressLogger = progressLogger;
			_mapper = mapper;
		}

		public async Task<ResultResponse<IEnumerable<PostGetDto>>> GetPostsAsync()
		{
			try
			{
				var posts = await _postsViewRepository.GetAsync();
				var postDtos = _mapper.Map<IEnumerable<PostGetDto>>(posts);
				return ResultResponse<IEnumerable<PostGetDto>>.GetSuccessResponse(postDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetPostsAsync));
				return ResultResponse<IEnumerable<PostGetDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<PagingData<PostGetDto>>> GetPagingPostsAsync(int numberPage, int pageSize)
		{
			try
			{
				var posts = await _postsViewRepository.GetAsync($"LIMIT {pageSize} OFFSET {(numberPage - 1) * pageSize}");
				var postDtos = _mapper.Map<IEnumerable<PostGetDto>>(posts);
				var count = await _postsViewRepository.GetCountAsync();

				return ResultResponse<PagingData<PostGetDto>>.GetSuccessResponse(new PagingData<PostGetDto>(
					postDtos.ToArray(),
					numberPage,
					count
				));
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { numberPage, pageSize }, GetType().Name, nameof(GetPagingPostsAsync));
				return ResultResponse<PagingData<PostGetDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<PostGetDto>> GetPostAsync(int id)
		{
			try
			{
				var post = await _postsViewRepository.GetAsync(id);
				if (post == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound);
				}
				var postDto = _mapper.Map<PostGetDto>(post);
				return ResultResponse<PostGetDto>.GetSuccessResponse(postDto);
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
				var creator = await _usersRepository.GetAsync(creatorId);
				if (creator == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}

				var post = _mapper.Map<Post>(dto);
				post.creator_id = creatorId;
				post.created = DateTime.Now;
				var added = await _postsRepository.AddAsync(post);
				if (added == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.InternalServerError, "Не удалось добавить пост");
				}
				var addedView = await _postsViewRepository.GetAsync(post.id);
				var addedViewDto = _mapper.Map<PostGetDto>(addedView);

				return ResultResponse<PostGetDto>.GetSuccessResponse(addedViewDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, creatorId }, GetType().Name, nameof(CreatePostAsync));
				return ResultResponse<PostGetDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<PostGetDto>> UpdatePostAsync(int postId, PostEditDto dto, int editorId)
		{
			try
			{
				var editor = await _usersRepository.GetAsync(editorId);
				if (editor == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}

				var post = await _postsRepository.GetAsync(postId);
				if (post == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound, "Не найден пост");
				}

				post.text = dto.Text;
				post.title = dto.Title;
				post.edited = DateTime.Now;
				post.editor_id = editor.id;

				var updated = await _postsRepository.UpdateAsync(post);
				if (updated == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.InternalServerError, "Не удалось отредактировать пост");
				}

				var editedView = await _postsViewRepository.GetAsync(updated.id);
				var editedViewDto = _mapper.Map<PostGetDto>(editedView);

				return ResultResponse<PostGetDto>.GetSuccessResponse(editedViewDto);
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
				var success = await _postsRepository.DeleteAsync(id);
				var statusCode = success ? StatusCode.OK : StatusCode.NotFound;
				return new ResultResponse(isSuccess: success, statusCode: statusCode);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, id, GetType().Name, nameof(DeletePostAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
