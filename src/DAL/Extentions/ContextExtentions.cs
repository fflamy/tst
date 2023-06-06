using System.Diagnostics.CodeAnalysis;
using ApiProject.Core.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.DAL.Extentions
{
	/// <summary>
	/// Класс расширейний для работы с <see cref="ApiDbContext"/>.
	/// </summary>
	public static class ContextExtentions
	{
		/// <summary>
		/// Возвращает результаты запроса для заданной страницы.
		/// </summary>
		/// <typeparam name="T">Тип объекта БД.</typeparam>
		/// <param name="source">Исходный источник данных.</param>
		/// <param name="range">Параметры страницы.</param>
		/// <returns>Запрос с добавленным фильтром на выборку заданной страницы.</returns>
		public static IQueryable<T> Paginated<T>(
			[NotNull] this IQueryable<T> source,
			[NotNull] IPaginatedRequest range)
			=> source.Skip(range.Skip ?? 0).Take(range.Take ?? 10);

		/// <summary>
		/// Возвращает результаты запроса для заданной страницы.
		/// </summary>
		/// <typeparam name="T">Тип объекта БД.</typeparam>
		/// <param name="source">Исходный источник данных.</param>
		/// <param name="range">Параметры страницы.</param>
		/// <param name="token">Cancellation token.</param>
		/// <returns>Запрос с добавленным фильтром на выборку заданной страницы.</returns>
		public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
			[NotNull] this IQueryable<T> source,
			[NotNull] IPaginatedRequest range,
			CancellationToken token = default)
		{
			var count = await source.CountAsync(token);
			var items = await source.Paginated(range).ToListAsync(token);
			return new PaginatedResult<T>(items, count);
		}
	}
}
