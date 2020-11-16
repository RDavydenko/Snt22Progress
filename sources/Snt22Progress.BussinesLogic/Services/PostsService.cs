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
		private readonly IViewRepository<PostView, int> _postsViewRepository;
		private readonly IRepository<User, int> _usersRepository;
		private readonly IProgressLogger _progressLogger;
		private readonly IMapper _mapper;

		public PostsService(IRepository<Post, int> postsRepository,
			IViewRepository<PostView, int> postsViewRepository,
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
				var creator = await _usersRepository.GetAsync(creatorId);
				if (creator == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}

				var post = _mapper.Map<Post>(dto);
				post.Creator_Id = creatorId;
				post.Created = DateTime.Now;
				var added = await _postsRepository.AddAsync(post);
				if (added == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.InternalServerError, "Не удалось добавить пост");
				}
				var addedView = await _postsViewRepository.GetAsync(post.Id);
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

				post.Text = dto.Text;
				post.Title = dto.Title;
				post.Edited = DateTime.Now;
				post.Editor_Id = editor.Id;

				var updated = await _postsRepository.UpdateAsync(post);
				if (updated == null)
				{
					return ResultResponse<PostGetDto>.GetBadResponse(StatusCode.InternalServerError, "Не удалось отредактировать пост");
				}

				var editedView = await _postsViewRepository.GetAsync(updated.Id);
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
