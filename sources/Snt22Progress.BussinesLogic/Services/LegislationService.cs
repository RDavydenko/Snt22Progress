using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Legislations;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис для работы с законодательством
	/// </summary>
	public class LegislationService : ILegislationService
	{
		private readonly IMapper _mapper;
		private readonly IProgressLogger _progressLogger;
		private readonly IRepository<Legislation, int> _legislationsRepository;
		private readonly IRepository<User, int> _usersRepository;

		public LegislationService(IMapper mapper,
			IProgressLogger progressLogger,
			IRepository<Legislation, int> legislationsRepository,
			IRepository<User, int> usersRepository)
		{
			_mapper = mapper;
			_progressLogger = progressLogger;
			_legislationsRepository = legislationsRepository;
			_usersRepository = usersRepository;
		}

		public async Task<ResultResponse<IEnumerable<LegislationDto>>> GetListAsync()
		{
			try
			{
				var legislations = await _legislationsRepository.GetAsync();
				var legislationDtos = _mapper.Map<IEnumerable<LegislationDto>>(legislations);
				return ResultResponse<IEnumerable<LegislationDto>>.GetSuccessResponse(legislationDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetListAsync));
				return ResultResponse<IEnumerable<LegislationDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<LegislationDto>> AddAsync(LegislationCreateDto createDto, int creatorId)
		{
			try
			{
				var creator = await _usersRepository.GetAsync(creatorId);
				if (creator == null)
				{
					return ResultResponse<LegislationDto>.GetBadResponse(StatusCode.NotFound, "Пользователь с таким идентификатором не найден");
				}
				var legislation = _mapper.Map<Legislation>(createDto);
				legislation.creator_id = creatorId;
				legislation.created = DateTime.Now;
				var added = await _legislationsRepository.AddAsync(legislation);
				if (added == null)
				{
					return ResultResponse<LegislationDto>.GetInternalServerErrorResponse();
				}
				var newLegislationDto = _mapper.Map<LegislationDto>(added);
				return ResultResponse<LegislationDto>.GetSuccessResponse(newLegislationDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { createDto, creatorId }, GetType().Name, nameof(AddAsync));
				return ResultResponse<LegislationDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> DeleteAsync(int legislationId)
		{
			try
			{
				var legislation = await _legislationsRepository.GetAsync(legislationId);
				if (legislation == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound);
				}
				var success = await _legislationsRepository.DeleteAsync(legislationId);				
				return new ResultResponse(isSuccess: success, statusCode: success ? StatusCode.OK : StatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { legislationId }, GetType().Name, nameof(DeleteAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
