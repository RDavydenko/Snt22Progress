using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snt22Progress.DataAccess.Infrastructure
{
	/// <summary>
	/// Интерфейс репозитория
	/// </summary>
	/// <typeparam name="TEntity">Тип данных сущности, реализующий интерфейс <see cref="IBaseEntity{TId}"/></typeparam>
	/// <typeparam name="TId">Тип данных идентификатора сущности</typeparam>
	public interface IViewRepository<TEntity, TId>
		where TEntity : IBaseEntity<TId>, new()
	{
		/// <summary>
		/// Получить сущность по уникальному идентификатору
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Сущность</returns>
		Task<TEntity> GetAsync(TId id);

		/// <summary>
		/// Получить список сущностей по заданному поисковому запросу
		/// </summary>
		/// <param name="query">Поисковый запрос</param>
		/// <returns>Список сущностей</returns>
		Task<IEnumerable<TEntity>> GetAsync(string query = null);
	}
}
