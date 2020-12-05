using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class ValuePairsService : IValuePairsService
	{
		private readonly IRepository<ValuePair, int> _valuePairsRepository;
		private readonly IProgressLogger _progressLogger;

		public ValuePairsService(IRepository<ValuePair, int> valuePairsRepository,
			IProgressLogger progressLogger)
		{
			_valuePairsRepository = valuePairsRepository;
			_progressLogger = progressLogger;
		}

		public async Task<ResultResponse<string>> GetValueAsync(string key)
		{
			try
			{
				var value = (await _valuePairsRepository.GetAsync($"WHERE key='{key}'")).FirstOrDefault()?.value;
				return ResultResponse<string>.GetSuccessResponse(value);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, key, GetType().Name, nameof(GetValueAsync));
				return ResultResponse<string>.GetInternalServerErrorResponse();
			}
		}
	}
}
