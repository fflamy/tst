using System.Diagnostics.CodeAnalysis;
using ApiProject.Core;
using ApiProject.DAL;
using ApiProject.DAL.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Buisness.Objects
{
	/// <summary>
	/// Метод добавления объектов.
	/// </summary>
	public static class AddObjects
	{
		/// <summary>
		/// Запрос на добавление объектов.
		/// </summary>
		public class Command : IRequest
		{
			/// <summary>
			/// Список объектов.
			/// </summary>
			[NotNull]
			public List<KeyValuePair<int, string>> Objects { get; set; } = new ();
		}

		/// <inheritdoc/>
		public class Handler : IRequestHandler<Command>
		{
			private readonly IMapper _mapper;
			private readonly ApiDbContext _context;

			/// <summary>
			/// Конструктор <see cref="Handler"/> .
			/// </summary>
			/// <param name="context"> Контекст БД.</param>
			/// <param name="mapper"> Маппер.</param>
			public Handler(ApiDbContext context, IMapper mapper)
			{
				_mapper = mapper;
				_context = context;
			}

			/// <summary>
			/// Обработчик метода.
			/// </summary>
			/// <param name="request"><see cref="Command"/>.</param>
			/// <param name="cancellationToken">Токен отмены.</param>
			/// <returns>Асинхронная задача.</returns>
			public async Task Handle(Command request, CancellationToken cancellationToken)
			{
				_context.ClearTable<ApiObject>();
				var objects = _mapper.Map<List<ApiObject>>(request.Objects);
				if (objects.Any())
				{
					_context.AddRange(objects);
					await _context.SaveChangesAsync(cancellationToken);
				}
			}
		}
	}
}
