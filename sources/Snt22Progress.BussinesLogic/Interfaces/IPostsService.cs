using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Posts;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Сервис для работы с постами (новостями на главной странице)
	/// </summary>
	public interface IPostsService
	{
		Task<ResultResponse<IEnumerable<PostGetDto>>> GetPostsAsync();

		Task<ResultResponse<PostGetDto>> GetPostAsync(int id);

		Task<ResultResponse<PostGetDto>> CreatePostAsync(PostCreateDto dto, int authorId);

		Task<ResultResponse<PostGetDto>> UpdatePostAsync(int postId, PostEditDto dto, int editorId);

		Task<ResultResponse> DeletePostAsync(int id);		
	}
}
