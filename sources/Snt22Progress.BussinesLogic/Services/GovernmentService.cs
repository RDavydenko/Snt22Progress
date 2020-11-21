using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Users;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис для работы с правлением
	/// </summary>
	public class GovernmentService : IGovernmentService
	{
		private readonly IMapper _mapper;
		private readonly IProgressLogger _progressLogger;
		private readonly IRepository<User, int> _usersRepository;

		public GovernmentService(IMapper mapper, IProgressLogger progressLogger, IRepository<User, int> usersRepository)
		{
			_mapper = mapper;
			_progressLogger = progressLogger;
			_usersRepository = usersRepository;
		}

		public async Task<ResultResponse<IEnumerable<UserDto>>> GetMembersAsync()
		{
			try
			{
				var users = await _usersRepository.GetAsync("where is_government_member = TRUE");
				var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
				return ResultResponse<IEnumerable<UserDto>>.GetSuccessResponse(userDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetMembersAsync));
				return ResultResponse<IEnumerable<UserDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> AddMemberAsync(int userId)
		{
			try
			{
				var user = await _usersRepository.GetAsync(userId);
				if (user != null)
				{
					user.is_government_member = true;
					var updated = await _usersRepository.UpdateAsync(user);
					if (updated == null)
					{
						return ResultResponse.GetInternalServerErrorResponse();
					}
					return ResultResponse.GetSuccessResponse();
				}
				else
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { userId }, GetType().Name, nameof(AddMemberAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> DeleteMemberAsync(int userId)
		{
			try
			{
				var user = await _usersRepository.GetAsync(userId);
				if (user != null)
				{
					user.is_government_member = false;
					var updated = await _usersRepository.UpdateAsync(user);
					if (updated == null)
					{
						return ResultResponse.GetInternalServerErrorResponse();
					}
					return ResultResponse.GetSuccessResponse();
				}
				else
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { userId }, GetType().Name, nameof(DeleteMemberAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
