using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Documents;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.FileManager.Infrasructure;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class DocumentsService : IDocumentsService
	{
		private readonly IRepository<Document, int> _documentsRepository;
		private readonly IViewRepository<DocumentView, int> _documentViewsRepository;
		private readonly ConfigurationService _configurationService;
		private readonly IRepository<User, int> _usersRepository;
		private readonly IProgressLogger _progressLogger;
		private readonly IMapper _mapper;		

		public DocumentsService(IRepository<Document, int> documentsRepository,
			IProgressLogger progressLogger,
			IMapper mapper,
			IRepository<User, int> usersRepository,
			IViewRepository<DocumentView, int> documentViewsRepository,
			ConfigurationService configurationService)
		{
			_documentsRepository = documentsRepository;
			_progressLogger = progressLogger;
			_mapper = mapper;
			_usersRepository = usersRepository;
			_documentViewsRepository = documentViewsRepository;
			_configurationService = configurationService;
		}

		public async Task<ResultResponse<DocumentDto>> GetDocumentAsync(int documentId)
		{
			try
			{
				var document = (await _documentViewsRepository.GetAsync($"WHERE id = {documentId} AND is_active = TRUE")).FirstOrDefault();
				if (document == null)
				{
					return ResultResponse<DocumentDto>.GetBadResponse(StatusCode.NotFound, "Не найден файл с указанным идентификатором");
				}
				var documentDto = _mapper.Map<DocumentDto>(document);
				return ResultResponse<DocumentDto>.GetSuccessResponse(documentDto);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, documentId, GetType().Name, nameof(GetDocumentAsync));
				return ResultResponse<DocumentDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<IEnumerable<DocumentDto>>> GetDocumentsAsync()
		{
			try
			{
				var documents = await _documentViewsRepository.GetAsync($"WHERE is_active = TRUE");
				var documentDtos = _mapper.Map<IEnumerable<DocumentDto>>(documents);
				return ResultResponse<IEnumerable<DocumentDto>>.GetSuccessResponse(documentDtos);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, null, GetType().Name, nameof(GetDocumentsAsync));
				return ResultResponse<IEnumerable<DocumentDto>>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse<DocumentDto>> AddDocumentAsync(DocumentCreateDto dto, int userId)
		{
			try
			{
				var uploader = await _usersRepository.GetAsync(userId);
				if (uploader == null)
				{
					return ResultResponse<DocumentDto>.GetBadResponse(StatusCode.NotFound, "Не найден пользователь");
				}

				byte[] bytes = new byte[dto.File.Length];
				using (var stream = dto.File.OpenReadStream())
				{
					await stream.ReadAsync(bytes, 0, (int)dto.File.Length);
				}
				var fileManager = new FileManager.Infrasructure.FileManager(
					folder: _configurationService.UploadedFilesSettings.DocumentsFilesFolderRelativePath,
					progressLogger: _progressLogger);
				var uploadingResult = await fileManager.UploadFileAsync(new FileDto(dto.File.FileName, bytes),
					new string[] { "png", "jpg", "gif", "jpeg", "bmp" });

				if (uploadingResult.IsSuccess)
				{
					var document = await _documentsRepository.AddAsync(new Document
					{
						length = (int)dto.File.Length,
						name = dto.Name,
						native_name = dto.File.FileName,
						path = uploadingResult.FilePath,
						created = DateTime.Now,
						creator_id = userId,
					});

					if (document != null)
					{
						var documentView = await _documentViewsRepository.GetAsync(document.id);
						var documentDto = _mapper.Map<DocumentDto>(documentView);
						return ResultResponse<DocumentDto>.GetSuccessResponse(documentDto);
					}
					else
					{
						return ResultResponse<DocumentDto>.GetInternalServerErrorResponse();
					}
				}
				else
				{
					return ResultResponse<DocumentDto>.GetInternalServerErrorResponse(uploadingResult.ErrorDescription);
				}
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { dto, userId }, GetType().Name, nameof(AddDocumentAsync));
				return ResultResponse<DocumentDto>.GetInternalServerErrorResponse();
			}
		}

		public async Task<ResultResponse> RemoveDocumentAsync(int documentId)
		{
			try
			{
				var document = await _documentsRepository.GetAsync(documentId);
				if (document == null)
				{
					return ResultResponse.GetBadResponse(StatusCode.NotFound, "Не найден файл по этому идентификатору");
				}

				document.is_active = false;
				var updated = await _documentsRepository.UpdateAsync(document);
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
				_progressLogger.Error(ex, documentId, GetType().Name, nameof(RemoveDocumentAsync));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
