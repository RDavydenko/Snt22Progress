using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Questions;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Contracts.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис для работы с опросами
	/// </summary>
	public class QuestionsService : IQuestionsService
	{
		private readonly IMapper _mapper;
		private readonly IProgressLogger _progressLogger;
		private readonly IRepository<Question, int> _questionsRepository;
		private readonly IViewRepository<QuestionView, int> _questionsViewRepository;
		private readonly IRepository<Choise, int> _choisesRepository;
		private readonly IRepository<UserToChoise, int> _userToChoisesRepository;
		private readonly IRepository<User, int> _usersRepository;

		public QuestionsService(IMapper mapper,
			IProgressLogger progressLogger,
			IRepository<Question, int> questionsRepository,
			IViewRepository<QuestionView, int> questionsViewRepository,
			IRepository<Choise, int> choisesRepository,
			IRepository<UserToChoise, int> userToChoisesRepository,
			IRepository<User, int> usersRepository)
		{
			_mapper = mapper;
			_progressLogger = progressLogger;
			_questionsRepository = questionsRepository;
			_questionsViewRepository = questionsViewRepository;
			_choisesRepository = choisesRepository;
			_userToChoisesRepository = userToChoisesRepository;
			_usersRepository = usersRepository;
		}

		public async Task<ResultResponse<IEnumerable<QuestionDto>>> GetQuestionListAsync(int userId)
		{
			try
			{
				var user = await _usersRepository.GetAsync(userId);
				if (user == null)
				{
					return ResultResponse<IEnumerable<QuestionDto>>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}
				var questionViews = await _questionsViewRepository.GetAsync("WHERE is_active = TRUE");
				var questionMiddleDtos = _mapper.Map<IEnumerable<QuestionFullDto>>(questionViews); // Конвертируем в промежуточный ДТО
				var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questionMiddleDtos);
				foreach (var q in questionDtos)
				{
					q.IsVoted = await IsUserVotedInQuestion(userId, q.Id);
				}
				return ResultResponse<IEnumerable<QuestionDto>>.GetSuccessResponse(questionDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { userId }, GetType().Name, nameof(GetQuestionListAsync));
				return ResultResponse<IEnumerable<QuestionDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<QuestionDto>> AddQuestionAsync(QuestionCreateDto dto, int userId)
		{
			try
			{
				// Тексты вариантов не должны содержать | , т.к. этот символ используется в STRING_AGG в progress.questionsview
				// != false, потому что может быть как true, так и null, и эти оба варианта нам не подходят = ошибка
				if (dto?.Choises?.Any(x => x.Text.Contains('|')) != false)
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.NotFound, "Тексты вариантов ответа не должны содержать знак |");
				}

				var user = await _usersRepository.GetAsync(userId);
				if (user == null)
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}
				var question = _mapper.Map<Question>(dto);
				question.creator_id = userId;
				question.created = DateTime.Now;

				var addedQuestion = await _questionsRepository.AddAsync(question);
				if (addedQuestion == null)
				{
					return ResultResponse<QuestionDto>.GetInternalServerErrorResponse("Произошла ошибка при добавлении опроса");
				}

				var choises = _mapper.Map<IEnumerable<Choise>>(dto.Choises);
				foreach (var ch in choises)
				{
					ch.question_id = addedQuestion.id;
					var addedChoise = await _choisesRepository.AddAsync(ch);
					if (addedChoise == null)
					{
						return ResultResponse<QuestionDto>.GetInternalServerErrorResponse("Произошла ошибка при добавлении варианта ответа");
					}
				}

				var questionView = await _questionsViewRepository.GetAsync(question.id);
				var questionMiddleDto = _mapper.Map<QuestionFullDto>(questionView); // Конвертируем в промежуточный ДТО
				var addedQuestionDto = _mapper.Map<QuestionDto>(questionMiddleDto);
				return ResultResponse<QuestionDto>.GetSuccessResponse(addedQuestionDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, userId }, GetType().Name, nameof(AddQuestionAsync));
				return ResultResponse<QuestionDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> RemoveQuestionAsync(int id)
		{
			try
			{
				var question = await _questionsRepository.GetAsync(id);
				if (question == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound, "Опрос не найден");
				}
				question.is_active = false;
				var success = (await _questionsRepository.UpdateAsync(question)) != null;
				return new ResultResponse(isSuccess: success, statusCode: success ? StatusCode.OK : StatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { id }, GetType().Name, nameof(RemoveQuestionAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<QuestionDto>> VoteAsync(int id, int voteId, int voterId)
		{
			try
			{
				var voter = await _usersRepository.GetAsync(voterId);
				if (voter == null)
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}

				var question = (await _questionsRepository.GetAsync($"WHERE id = {id} AND is_active = TRUE")).FirstOrDefault();
				if (question == null)
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.NotFound, "Опрос не найден");
				}

				var choise = await _choisesRepository.GetAsync(voteId);
				if (choise == null)
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.NotFound, "Вариант ответа не найден");
				}

				if (await IsUserVotedInQuestion(voterId, id)) // Если уже принимал участие
				{
					return ResultResponse<QuestionDto>.GetBadResponse(StatusCode.Forbidden, "Пользователь уже принимал участие в этом опросе");
				}
				else
				{
					await _userToChoisesRepository.AddAsync(
						new UserToChoise { choise_id = voteId, user_id = voterId }
					);
					choise.votes_count++;
					await _choisesRepository.UpdateAsync(choise);
					var questionView = await _questionsViewRepository.GetAsync(id);
					var questionMiddleDto = _mapper.Map<QuestionFullDto>(questionView); // Конвертируем в промежуточный ДТО
					var questionDto = _mapper.Map<QuestionDto>(questionMiddleDto);
					questionDto.IsVoted = true;
					return ResultResponse<QuestionDto>.GetSuccessResponse(questionDto);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { id, voteId, voterId }, GetType().Name, nameof(VoteAsync));
				return ResultResponse<QuestionDto>.GetInternalServerErrorResponse();
			}
		}

		/// <summary>
		/// Принимал ли пользователь участие в этом опросе
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="questionId">Идентификатор опроса</param>
		/// <returns><see langword="true"/>, если уже голосовал</returns>
		private async Task<bool> IsUserVotedInQuestion(int userId, int questionId)
		{
			var isVoted = false;

			// Если по этому опросу пользователь уже проголосовал, то о нем будет запись в таблице progress.usertochoises
			// для соответствующего choise (выбора)
			var choiseIds = (await _choisesRepository.GetAsync($"WHERE question_id = {questionId}")).Select(x => x.id);
			if (choiseIds.Count() > 0)
			{
				var userToChoises = await _userToChoisesRepository.GetAsync($"WHERE user_id={userId} AND choise_id IN ({string.Join(", ", choiseIds)})");
				isVoted = userToChoises.Any();
			}

			return isVoted;
		}
	}
}
