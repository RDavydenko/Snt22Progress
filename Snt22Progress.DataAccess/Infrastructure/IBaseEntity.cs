using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.DataAccess.Infrastructure
{
	/// <summary>
	/// Интерфейс базовой сущности в БД (базе данных)
	/// </summary>
	/// <typeparam name="TId">Уникальный идентификатор сущности</typeparam>
	public interface IBaseEntity<TId>
	{
		/// <summary>
		/// Уникальный идентификатор сущности
		/// </summary>
		TId Id { get; set; }
	}
}
