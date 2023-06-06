using ApiProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiProject.DAL
{
	/// <summary>
	/// Контекст базы данных.
	/// </summary>
	public class ApiDbContext : DbContext
	{
		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="options">Параметры подключения.</param>
		public ApiDbContext(DbContextOptions<ApiDbContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Таблица объектов.
		/// </summary>
		public DbSet<ApiObject> ApiObjects { get; set; }

		/// <summary>
		/// Очищает таблицу.
		/// </summary>
		/// <typeparam name="TEntity">тип данных в таблице.</typeparam>
		public void ClearTable<TEntity>()
			where TEntity : class
		{
			Set<TEntity>().RemoveRange(Set<TEntity>());
			ResetSequence(GetTableName<TEntity>());
			SaveChanges();
		}

		private void ResetSequence(string tableName)
		{
			Database.ExecuteSqlRaw($"UPDATE sqlite_sequence SET seq = 0 WHERE name='{tableName}';");
		}

		private string GetTableName<TEntity>()
			where TEntity : class
		{
			IEntityType entityType = Model.FindEntityType(typeof(TEntity)) !;
			return entityType.GetTableName() !;
		}
	}
}