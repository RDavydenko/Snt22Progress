using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Advertisements;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.FileManager.Infrasructure;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	/// <summary>
	/// Сервис для работы с объявлениями о продаже участков
	/// </summary>
	public class AdvertisementsService : IAdvertisementsService
	{
		private readonly IMapper _mapper;
		private readonly IProgressLogger _progressLogger;
		private readonly IRepository<Advertisement, int> _advertisementsRepository;
		private readonly IRepository<AdvertisementFile, int> _advertisementFilesRepository;
		private readonly IViewRepository<AdvertisementView, int> _advertisementViewsRepository;
		private readonly IRepository<User, int> _usersRepository;
		private readonly ConfigurationService _configurationService;

		public AdvertisementsService(IMapper mapper,
			IProgressLogger progressLogger,
			IRepository<Advertisement, int> advertisementsRepository,
			IRepository<AdvertisementFile, int> advertisementFilesRepository,
			IViewRepository<AdvertisementView, int> advertisementViewsRepository,
			IRepository<User, int> usersRepository,
			ConfigurationService configurationService)
		{
			_mapper = mapper;
			_progressLogger = progressLogger;
			_advertisementsRepository = advertisementsRepository;
			_advertisementFilesRepository = advertisementFilesRepository;
			_advertisementViewsRepository = advertisementViewsRepository;
			_usersRepository = usersRepository;
			_configurationService = configurationService;
		}

		public async Task<ResultResponse<IEnumerable<AdvertisementDto>>> GetAdvertisementListAsync()
		{
			try
			{
				var advertisementViews = await _advertisementViewsRepository.GetAsync("WHERE is_active = TRUE");
				var advertisementDtos = _mapper.Map<IEnumerable<AdvertisementDto>>(advertisementViews);
				return ResultResponse<IEnumerable<AdvertisementDto>>.GetSuccessResponse(advertisementDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetAdvertisementListAsync));
				return ResultResponse<IEnumerable<AdvertisementDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<AdvertisementDto>> GetAdvertisementAsync(int id)
		{
			try
			{
				var advertisementView = (await _advertisementViewsRepository.GetAsync($"WHERE id = {id} AND is_active = TRUE")).FirstOrDefault();
				if (advertisementView == null)
				{
					return ResultResponse<AdvertisementDto>.GetBadResponse(StatusCode.NotFound);
				}
				var advertisementDto = _mapper.Map<AdvertisementDto>(advertisementView);
				return ResultResponse<AdvertisementDto>.GetSuccessResponse(advertisementDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { id }, GetType().Name, nameof(GetAdvertisementAsync));
				return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<AdvertisementDto>> AddAdvertisementAsync(AdvertisementCreateDto dto, int creatorId)
		{
			try
			{
				int? imageId = null; // Идентификатор изображения (если прикреплеено)
				var creator = await _usersRepository.GetAsync(creatorId);
				if (creator == null)
				{
					return ResultResponse<AdvertisementDto>.GetBadResponse(StatusCode.NotFound, "Пользователь не найден");
				}
				if (dto.Image != null) // Загрузить картинку (опционально)
				{
					var fileManager = new FileManager.Infrasructure.FileManager(
						baseAddress: _configurationService.WebAppSettings.BaseAddress,
						folder: _configurationService.UploadedFilesSettings.AdvertisementFilesFolderRelativePath,
						progressLogger: _progressLogger);
					var bytes = new byte[dto.Image.Length];
					using (var stream = dto.Image.OpenReadStream())
					{
						await stream.ReadAsync(bytes, 0, bytes.Length);
					}
					var uploadingResult = await fileManager.UploadFileAsync(
						file: new FileDto(dto.Image.FileName, bytes),
						allowedFormats: new string[] { "png", "jpg", "gif", "jpeg", "bmp" });
					if (uploadingResult.IsSuccess) // Если удалось загрузить изображение, то сохраняем в БД
					{
						var newAdvertisementFile = new AdvertisementFile
						{
							length = bytes.Length,
							native_name = dto.Image.FileName,
							path = uploadingResult.FilePath,
							uploaded = DateTime.Now,
							uploader_id = creatorId
						};
						var addedFile = await _advertisementFilesRepository.AddAsync(newAdvertisementFile);
						if (addedFile == null)
						{
							return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse("Не удалось загрузить изображение");
						}
						else // Если загрузили изображение, то сохраняем его id
						{
							imageId = addedFile.id;
						}
					}
					else
					{
						return ResultResponse<AdvertisementDto>.GetBadResponse(StatusCode.BadRequest, uploadingResult.ErrorDescription);
					}
				}
				var advertisement = _mapper.Map<Advertisement>(dto);
				advertisement.created = DateTime.Now;
				advertisement.creator_id = creatorId;
				advertisement.image_file_id = imageId;
				var added = await _advertisementsRepository.AddAsync(advertisement);
				if (added == null)
				{
					return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse("Произошла ошибка при добавлении объявления");
				}
				else
				{
					var addedView = await _advertisementViewsRepository.GetAsync(added.id);
					var addedDto = _mapper.Map<AdvertisementDto>(addedView);
					return ResultResponse<AdvertisementDto>.GetSuccessResponse(addedDto);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, creatorId }, GetType().Name, nameof(AddAdvertisementAsync));
				return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<AdvertisementDto>> EditAdvertisementAsync(int id, AdvertisementEditDto dto)
		{
			try
			{
				var advertisement = await _advertisementsRepository.GetAsync(id);
				if (advertisement == null)
				{
					return ResultResponse<AdvertisementDto>.GetBadResponse(StatusCode.NotFound, "Объявление не найдено");
				}
				advertisement.text = dto.Text;
				advertisement.title = dto.Title;
				advertisement.price = dto.Price;
				advertisement.square = dto.Square;
				advertisement.is_privatizated = dto.IsPrivatizated;
				var edited = await _advertisementsRepository.UpdateAsync(advertisement);
				if (edited != null)
				{
					var editedView = await _advertisementViewsRepository.GetAsync(edited.id);
					var editedDto = _mapper.Map<AdvertisementDto>(editedView);
					return ResultResponse<AdvertisementDto>.GetSuccessResponse(editedDto);
				}
				else
				{
					return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse("Произошла неизвестная ошибка при редактировании объявления");
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { id, dto }, GetType().Name, nameof(EditAdvertisementAsync));
				return ResultResponse<AdvertisementDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> RemoveAdvertisementAsync(int id)
		{
			try
			{
				var advertisement = await _advertisementsRepository.GetAsync(id);
				if (advertisement == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound, "Объявление не найдено");
				}
				advertisement.is_active = false;
				var success = (await _advertisementsRepository.UpdateAsync(advertisement)) != null;
				return new ResultResponse(isSuccess: success, statusCode: success ? StatusCode.OK : StatusCode.InternalServerError);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { id }, GetType().Name, nameof(RemoveAdvertisementAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
