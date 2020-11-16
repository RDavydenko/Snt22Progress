using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snt22Progress.DataAccess.Infrastructure
{
	/// <summary>
	/// Абстрактный класс для работы с SQL базами данных
	/// </summary>
	public abstract class BaseSqlRepository<TEntity> : IRepository<TEntity, int>
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

		/// <summary>
		/// Названия всех полей сущности кроме PK
		/// </summary>
		protected readonly string[] _noPkFieldNames;

		private Type _entityType;

		public BaseSqlRepository(string connection)
		{
			_connection = connection;
			_entityType = typeof(TEntity);
			_allFieldNames = GetAllFieldNames();
			_noPkFieldNames = GetAllNoPkFieldNames();
		}

		protected abstract IDbConnection GetNewConnection();

		protected string GetBaseSqlString(SqlStringType sqlStringType, TEntity entity = default)
		{
			var sb = new StringBuilder();
			switch (sqlStringType)
			{
				case SqlStringType.Select:
					sb.AppendLine($"SELECT {GetSelectValues()}");
					sb.AppendLine($"FROM {TablePath}");
					break;
				case SqlStringType.Insert:
					sb.AppendLine($"INSERT INTO {TablePath} ({GetNameColumnsForInsert()})");
					sb.AppendLine($"OUTPUT INSERTED.[Id]");
					sb.AppendLine($"VALUES ({GetValuesForInsert(entity)})");
					break;
				case SqlStringType.Update:
					sb.AppendLine($"UPDATE {TablePath}");
					sb.AppendLine($"SET {GetValuesForUpdate(entity)}");
					sb.AppendLine($"WHERE id = {entity.Id}");
					break;
				case SqlStringType.Delete:
					sb.AppendLine($"DELETE FROM {TablePath}");
					break;
			}
			return sb.ToString();
		}

		private string[] GetAllFieldNames()
		{
			return _entityType.GetProperties().Select(x => x.Name.ToLower()).ToArray();
		}

		private string[] GetAllNoPkFieldNames()
		{
			TEntity entity;
			return _entityType.GetProperties().Select(x =>
			{
				if (x.Name == nameof(entity.Id)) // Идентификатор не устанавливаем в INSERT
				{
					return null;
				}
				else
				{
					return x.Name.ToLower();
				}
			}).Where(x => x != null).ToArray();
		}

		protected string GetSelectValues()
		{
			return string.Join(", ", _allFieldNames);
		}

		protected string GetNameColumnsForInsert()
		{
			return string.Join(", ", _noPkFieldNames);
		}

		protected string GetValuesForInsert(TEntity entity, char separator = '\'')
		{
			var props = entity.GetType().GetProperties();
			var values = _noPkFieldNames.Select(x =>
			{
				var prop = props.First(p => p.Name.ToLower() == x);

				if (prop.GetValue(entity) == null)
				{
					return "DEFAULT";
				}

				if (prop.PropertyType == typeof(string))
				{
					return $"{separator}{prop.GetValue(entity)}{separator}";
				}
				return prop.GetValue(entity).ToString();
			});
			return string.Join(", ", values);
		}

		protected string GetValuesForUpdate(TEntity entity, char separator = '\'')
		{
			var props = entity.GetType().GetProperties();
			var values = _noPkFieldNames.Select(x =>
			{
				var prop = props.First(p => p.Name.ToLower() == x);

				if (prop.GetValue(entity) == null)
				{
					return $"{x}=DEFAULT";
				}

				if (prop.PropertyType == typeof(string))
				{
					return $"{x}={separator}{prop.GetValue(entity)}{separator}";
				}
				return $"{x}={prop.GetValue(entity)}";
			});
			return string.Join(", ", values);
		}

		public async Task<TEntity> GetAsync(int id)
		{
			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var entity = await _dbConnection.GetAsync<TEntity>(id);
				_dbConnection.Close();
				return entity;
			}
		}

		public async Task<IEnumerable<TEntity>> GetAsync(string query = null)
		{
			var sql = GetBaseSqlString(SqlStringType.Select);
			var sb = new StringBuilder(sql);
			sb.AppendLine(query);

			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var entities = await _dbConnection.QueryAsync<TEntity>(sb.ToString());
				_dbConnection.Close();
				return entities;
			}
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var id = await _dbConnection.InsertAsync<TEntity>(entity);
				_dbConnection.Close();

				return await GetAsync(id);
			}
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var success = await _dbConnection.UpdateAsync(entity);
				_dbConnection.Close();

				return success ? 
					await GetAsync(entity.Id)
					: entity;
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await GetAsync(id);

			if (entity == null)
			{
				return false;
			}
			
			using (_dbConnection = GetNewConnection())
			{
				_dbConnection.Open();

				var success = await _dbConnection.DeleteAsync(entity);
				_dbConnection.Close();

				return success;
			}
		}
	}
}
