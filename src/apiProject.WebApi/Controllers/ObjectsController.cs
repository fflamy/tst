using ApiProject.Buisness.Objects;
using ApiProject.DAL.Entities;
using ApiProject.WebApi.Serializers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.WebApi.Controllers
{
	/// <inheritdoc/>
	[Route("api/[controller]")]
	[ApiController]
	public class ObjectsController : ControllerBase
	{
		private readonly IMediator _mediator;

		/// <summary>
		/// Конструктор контроллера.
		/// </summary>
		/// <param name="mediator"> Медиатор.</param>
		public ObjectsController(IMediator mediator)
			=> _mediator = mediator;

		/// <summary>
		/// Возвращает список объектов.
		/// </summary>
		/// <param name="request">запрос на получение объектов.</param>
		/// <returns>Список объектов.</returns>
		[HttpGet]
		public async Task<List<ApiObject>> Get([FromQuery] GetObjects.Command request) =>
			await _mediator.Send(request);

		/// <summary>
		///  Метод добавления объектов в таблицу.
		/// </summary>
		/// <param name="objects">Объекты.</param>
		/// <param name="cancellationToken">Токен отмены.</param>
		/// <returns> результат.</returns>
		[HttpPost]
		public async Task Post([FromBody][ModelBinder(BinderType = typeof(CustomKeyValuePairBinder))]List<KeyValuePair<int, string>> objects, CancellationToken cancellationToken)
		{
			var request = new AddObjects.Command { Objects = objects };
			await _mediator.Send(request, cancellationToken);
		}
	}
}
