using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Infrastructure
{
	/// <summary>
	/// Интерфейс DTO (Data Transfer Object)
	/// </summary>
	public interface IDto<TId>
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		TId Id { get; set; }
	}
}
