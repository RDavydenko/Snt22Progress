using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Documents;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Сервис для работы с документами
	/// </summary>
	public interface IDocumentsService
	{
		Task<ResultResponse<IEnumerable<DocumentDto>>> GetDocumentsAsync();

		Task<ResultResponse<DocumentDto>> GetDocumentAsync(int documentId);

		Task<ResultResponse<DocumentDto>> AddDocumentAsync(DocumentCreateDto dto, int userId);

		Task<ResultResponse> RemoveDocumentAsync(int documentId);
	}
}
