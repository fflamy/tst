using ApiProject.Core.Entities;
using ApiProject.Core.Pagination;
using ApiProject.DAL;
using ApiProject.DAL.Extentions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Buisness.Objects
{
    /// <summary>
    /// Получение списка объектов.
    /// </summary>
    public static class GetObjects
    {
        /// <summary>
        /// Запрос на пагинированный список объектов.
        /// </summary>
        public class Command : IPaginatedRequest, IRequest<List<ApiObject>>
        {
            /// <inheritdoc/>
            public int? Take { get; set; }

            /// <inheritdoc/>
            public int? Skip { get; set; }
        }

        /// <inheritdoc/>
        public class Handler : IRequestHandler<Command, List<ApiObject>>
        {
            private readonly ApiDbContext _context;

            /// <summary>
            /// Конструктор  <see cref="Handler"/>.
            /// </summary>
            /// <param name="context"> Контекст БД.</param>
            public Handler(ApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Метод получения списка объектов.
            /// </summary>
            /// <param name="request">запрос на получение списка.</param>
            /// <param name="cancellationToken">токен отмены.</param>
            /// <returns>Список объектов.</returns>
            public async Task<List<ApiObject>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _context
                    .ApiObjects
                    .Paginated(request)
                    .ToListAsync(cancellationToken);

                return result;
            }
		}
    }
}
