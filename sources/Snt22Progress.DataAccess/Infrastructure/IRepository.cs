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
	public interface IRepository<TEntity, TId>
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

		/// <summary>
		/// Добавить новую сущность в репозиторий
		/// </summary>
		/// <param name="entity">Новая сущность</param>
		/// <returns>Добавленная сущность</returns>
		Task<TEntity> AddAsync(TEntity entity);

		/// <summary>
		/// Обновить сущность в репозитории
		/// </summary>
		/// <param name="entity">Обновляемая сущность</param>
		/// <returns>Обновленная сущность</returns>
		Task<TEntity> UpdateAsync(TEntity entity);

		/// <summary>
		/// Удалить сущность из репозитория по уникальному идентификатору
		/// </summary>
		/// <param name="id">Уникальный идентификатор</param>
		/// <returns>Возвращает <see langword="true"/>, если сущность была удалена. Иначе <see langword="false"/></returns>
		Task<bool> DeleteAsync(TId id);
	}
}
