using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Snt22Progress.DataAccess.Infrastructure
{
	/// <summary>
	/// Базовый репозиторий для работы с представлениями
	/// </summary>
	public abstract class BaseSqlViewRepository<TEntity> :  IViewRepository<TEntity, int>
		where TEntity : class, IBaseEntity<int>, new()
	{
		protected readonly string _connection;

		/// <summary>
		/// Подключение к БД
		/// </summary>
		private IDbConnection _dbConnection;

		/// <summary>
		/// Название схемы
		/// </summary>
		public abstract string SchemaName { get; }

		/// <summary>
		/// Название таблицы
		/// </summary>
		public abstract string TableName { get; }

		/// <summary>
		/// Путь к таблице <схема>.<название_таблицы>
		/// </summary>
		public string TablePath => $"{SchemaName}.{TableName}";

		/// <summary>
		/// Названия всех полей сущности
		/// </summary>
		protected readonly string[] _allFieldNames;

		private Type _entityType;

		public BaseSqlViewRepository(string connection)
		{
			_connection = connection;
			_entityType = typeof(TEntity);
			_allFieldNames = GetAllFieldNames();
		}

		protected abstract IDbConnection GetNewConnection();

		private string[] GetAllFieldNames()
		{
			return _entityType.GetProperties().Select(x => x.Name.ToLower()).ToArray();
		}

		protected string GetSelectValues()
		{
			return string.Join(", ", _allFieldNames);
		}

		public async Task<TEntity> GetAsync(int id)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"SELECT {GetSelectValues()}");
			sb.AppendLine($"FROM {TablePath}");
			sb.AppendLine($"WHERE id = {id}");
			var sql = sb.ToString();

			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var entity = await _dbConnection.QuerySingleOrDefaultAsync<TEntity>(sql);
				_dbConnection.Close();
				return entity;
			}
		}

		public async Task<IEnumerable<TEntity>> GetAsync(string query = null)
		{
			var sb = new StringBuilder();
			sb.AppendLine($"SELECT {GetSelectValues()}");
			sb.AppendLine($"FROM {TablePath}");
			sb.AppendLine(query);
			var sql = sb.ToString();

			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var entities = await _dbConnection.QueryAsync<TEntity>(sql);
				_dbConnection.Close();
				return entities;
			}
		}
	}
}
