using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.DebtorFiles;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.FileManager.Infrasructure;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис для работы с файлами о должниках
	/// </summary>
	public class DebtorFilesService : IDebtorFilesService
	{
		private readonly IMapper _mapper;
		private readonly IProgressLogger _progressLogger;
		private readonly IRepository<DebtorFile, int> _debtorFilesRepository;
		private readonly IRepository<User, int> _usersRepository;
		private readonly ConfigurationService _configurationService;

		public DebtorFilesService(IMapper mapper,
			IProgressLogger progressLogger,
			IRepository<DebtorFile, int> debtorFilesRepository,
			IRepository<User, int> usersRepository,
			ConfigurationService configurationService)
		{
			_mapper = mapper;
			_progressLogger = progressLogger;
			_debtorFilesRepository = debtorFilesRepository;
			_usersRepository = usersRepository;
			_configurationService = configurationService;
		}

		public async Task<ResultResponse<IEnumerable<DebtorFileDto>>> GetListAsync()
		{
			try
			{
				var debtorFiles = await _debtorFilesRepository.GetAsync($"WHERE is_active = TRUE");
				var debtorFileDtos = _mapper.Map<IEnumerable<DebtorFileDto>>(debtorFiles);
				return ResultResponse<IEnumerable<DebtorFileDto>>.GetSuccessResponse(debtorFileDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetListAsync));
				return ResultResponse<IEnumerable<DebtorFileDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<DebtorFileDto>> GetDebtorFileAsync(int id)
		{
			try
			{
				var debtorFile = (await _debtorFilesRepository.GetAsync($"WHERE id = {id} AND is_active = TRUE")).FirstOrDefault();
				if (debtorFile == null)
				{
					return ResultResponse<DebtorFileDto>.GetBadResponse(StatusCode.NotFound, "Не найден файл с указанным идентификатором");
				}
				var debtorFileDto = _mapper.Map<DebtorFileDto>(debtorFile);
				return ResultResponse<DebtorFileDto>.GetSuccessResponse(debtorFileDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, id, GetType().Name, nameof(GetDebtorFileAsync));
				return ResultResponse<DebtorFileDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<DebtorFileDto>> AddDebtorFileAsync(DebtorFileCreateDto dto, int uploaderId)
		{
			try
			{
				var uploader = await _usersRepository.GetAsync(uploaderId);
				if (uploader == null)
				{
					return ResultResponse<DebtorFileDto>.GetBadResponse(StatusCode.NotFound, "Не найден пользователь");
				}

				byte[] bytes = new byte[dto.File.Length];
				using (var stream = dto.File.OpenReadStream())
				{
					await stream.ReadAsync(bytes, 0, (int)dto.File.Length);
				}
				var fileManager = new FileManager.Infrasructure.FileManager(
					baseAddress: _configurationService.WebAppSettings.BaseAddress,
					folder: _configurationService.UploadedFilesSettings.DebtorFilesFolderRelativePath,
					progressLogger: _progressLogger);
				var uploadingResult = await fileManager.UploadFileAsync(new FileDto(dto.File.FileName, bytes),
					new string[] { "png", "jpg", "gif", "jpeg", "bmp" });

				if (uploadingResult.IsSuccess)
				{
					var debtorFile = await _debtorFilesRepository.AddAsync(new DebtorFile
					{
						length = (int)dto.File.Length,
						name = dto.Name,
						native_name = dto.File.FileName,
						path = uploadingResult.FilePath,
						uploaded = DateTime.Now,
						uploader_id = uploaderId,
					});

					if (debtorFile != null)
					{
						var documentDto = _mapper.Map<DebtorFileDto>(debtorFile);
						return ResultResponse<DebtorFileDto>.GetSuccessResponse(documentDto);
					}
					else
					{
						return ResultResponse<DebtorFileDto>.GetInternalServerErrorResponse();
					}
				}
				else
				{
					return ResultResponse<DebtorFileDto>.GetInternalServerErrorResponse(uploadingResult.ErrorDescription);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, uploaderId }, GetType().Name, nameof(AddDebtorFileAsync));
				return ResultResponse<DebtorFileDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> RemoveDebtorFileAsync(int id)
		{
			try
			{
				var debtorFile = await _debtorFilesRepository.GetAsync(id);
				if (debtorFile == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound, "Не найден файл по этому идентификатору");
				}

				debtorFile.is_active = false;
				var updated = await _debtorFilesRepository.UpdateAsync(debtorFile);
				if (updated != null)
				{
					return ResultResponse.GetSuccessResponse();
				}
				else
				{
					return ResultResponse.GetInternalServerErrorResponse();
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, id, GetType().Name, nameof(RemoveDebtorFileAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
